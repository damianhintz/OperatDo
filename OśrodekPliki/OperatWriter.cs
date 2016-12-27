using System;
using System.Collections.Generic;
using System.Linq;
using OśrodekDomena;
using OśrodekDomena.Rozszerzenia;
using OśrodekFirebird;

namespace OśrodekPliki
{
    /// <summary>
    /// Importer zeskanowanych operatów do bazy danych Ośrodka.
    /// </summary>
    public class OperatWriter : IDisposable
    {
        public OśrodekOperatDb OperatDb { get; set; }
        public OśrodekPlikiDb PlikDb { get; set; }
        
        /// <summary>
        /// Odszukaj i wczytaj operat z bazy danych Ośrodka.
        /// </summary>
        /// <param name="operat"></param>
        /// <returns></returns>
        public bool WczytajOperat(Operat operat)
        {
            if (operat == null)
                throw new ArgumentNullException(
                    paramName: "operat",
                    message: "Operat jest null");
            if (operat.Id.HasValue) return true; //Nie odczytuj ponownie
            int? id;
            char? typ;
            if (!OperatDb.SzukajOperatu(operat.IdZasobu, out id, out typ)) return false;
            operat.Id = id.Value; //Zapamiętaj uid operatu
            operat.Typ = typ.Value; //Zapamiętaj typ operatu
            return true;
        }

        public bool WczytajDokumenty(IEnumerable<Operat> operaty)
        {
            if (!operaty.Any()) return false; //Brak operatów
            var idOperatów = operaty.Select(op => op.Id.Value).ToArray();
            var dokumentyOperatu = new List<int>();
            var operatyDokumentów = new List<int>();
            var result = OperatDb.SzukajDokumentów(idOperatów, dokumentyOperatu, operatyDokumentów);
            foreach (var operat in operaty) operat.Dokumenty = 0; //Wyzeruj liczbę dokumentów
            var indeksOperatów = operaty.ToDictionary(op => op.Id.Value);
            for(int i = 0; i < dokumentyOperatu.Count; i++)
            {
                var dokumentId = dokumentyOperatu[i];
                var operatId = operatyDokumentów[i];
                var operat = indeksOperatów[operatId];
                operat.Dokumenty++; //Odśwież liczbę dokumentów
            }
            return true;
        }

        public bool WczytajDokumenty(Operat operat)
        {
            if (operat == null)
                throw new ArgumentNullException(
                    paramName: "operat",
                    message: "Operat jest null");
            if (!operat.Id.HasValue)
                throw new InvalidOperationException(
                    message: "Operat nie został odnaleziony w bazie danych Ośrodka");
            if (operat.Dokumenty.HasValue) return true; //Nie odczytuj ponownie
            operat.Dokumenty = OperatDb
                .SzukajDokumentów(operat.Id.Value)
                .Count();
            return true;
        }

        /// <summary>
        /// Zapisz dokumenty i pliki operatu w bazie danych Ośrodka.
        /// </summary>
        /// <param name="operat"></param>
        public bool ZapiszOperat(Operat operat)
        {
            if (operat == null)
                throw new ArgumentNullException(
                    paramName: "operat",
                    message: "Operat jest null");
            if (!operat.Id.HasValue) return false; //Brak operatu w bazie danych Ośrodka
            //Czy zawiera jakieś dokumenty w bazie danych?
            if (!operat.Dokumenty.HasValue)
                throw new InvalidOperationException(
                    message: "Nie ustalono czy operat posiada dokumenty w bazie danych Ośrodka");
            if (operat.Dokumenty.Value > 0) return false; //Operat posiada już dokumenty
            var result = false;
            try
            {
                OperatDb.RozpocznijTransakcję();
                PlikDb.RozpocznijTransakcję();
                ZapiszDokumentyOperatu(operat);
                OperatDb.ZakończTransakcję();
                PlikDb.ZakończTransakcję();
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
                OperatDb.WycofajTransakcję();
                PlikDb.WycofajTransakcję();
            }
            finally
            {
                //op.ZapiszTransakcję(fileName: "P.operat.insert and P.delete.sql");
                //plik.ZapiszTransakcję(fileName: "P.plik.insert and P.delete.sql");
            }
            return result;
        }

        void ZapiszDokumentyOperatu(Operat operat)
        {
            foreach (var dokument in operat.Pliki) ZapiszDokumentOperatu(dokument);
        }

        void ZapiszDokumentOperatu(PlikOperatu dokument)
        {
            if (dokument.Id.HasValue) return; //Pomiń operaty, które posiadają już dokumenty w bazie
            //Odczytaj plik z dysku
            var plik = dokument.WczytajPlik();
            var plikId = ZapiszPlik(dokument, plik);
            dokument.Rozdzielczość = plik.OdczytajRozdzielczość();
            var dokumentId = ZapiszDokument(dokument);
        }

        int ZapiszPlik(PlikOperatu dokument, byte[] blob)
        {
            var plikId = PlikDb.DodajPlik(blob);
            dokument.PlikId = plikId; //Zapamiętaj id dodanego pliku
            return plikId;
        }

        int ZapiszDokument(PlikOperatu dokument)
        {
            var operat = dokument.Operat;
            var dokumentId = OperatDb.DodajDokument(
                operat.Id.Value, operat.Typ.Value,
                dokument.Plik, dokument.Rozdzielczość.Ppm,
                dokument.PlikId.Value);
            dokument.Id = dokumentId; //Zapamiętaj id dodanego dokumentu
            return dokumentId;
        }

        /// <summary>
        /// Wczytaj istniejące dokumenty dla znalezionych operatów.
        /// </summary>
        /// <remarks>Nie powinno być takich dokumentów.</remarks>
        /// <returns></returns>
        bool WczytajDokumenty(RepozytoriumOperatów repozytorium)
        {
            var idOperatów = repozytorium.ZnalezioneOperaty.Select(op => op.Id.Value);
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var count = OperatDb.SzukajDokumentów(idOperatów.ToArray(), dokumenty, operaty);
            if (count == 0) return false; //Nie znaleziono żadnych dokumentów
            //var operatyPosiadająceDokumenty = operaty.Distinct(); //Do wykluczenia
            //Dodaj znalezione dokumenty do operatów
            var znalezioneOperaty = repozytorium.ZnalezioneOperaty.ToDictionary(op => op.Id.Value);
            for (int i = 0; i < dokumenty.Count; i++)
            {
                var operatId = operaty[i];
                var dokumentId = dokumenty[i];
                var operat = znalezioneOperaty[operatId];
                var dokument = new PlikOperatu { Id = dokumentId };
                operat.Dodaj(dokument);
            }
            return true;
        }

        public void Dispose()
        {
            OperatDb.Dispose();
            PlikDb.Dispose();
        }
    }
}
