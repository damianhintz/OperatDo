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
    public class OperatWriter
    {
        public OśrodekOperatDb OperatDb { get; set; }
        public OśrodekPlikiDb PlikDb { get; set; }
        
        /// <summary>
        /// Odczytaj wybrane operaty.
        /// </summary>
        /// <param name="operaty"></param>
        /// <returns></returns>
        public bool WczytajOperaty(IEnumerable<Operat> operaty)
        {
            var pominięte = 0;
            foreach (var operat in operaty)
            {
                if (!WczytajOperat(operat))
                {
                    pominięte++;
                    continue; //Brak operatu w bazie Ośrodka
                }
            }
            return pominięte == 0;
        }

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
            if (operat.Id.HasValue) return true; //Operat został już odnaleziony
            int? id;
            char? typ;
            if (!OperatDb.SzukajOperatu(operat.IdZasobu, out id, out typ)) return false;
            operat.Id = id.Value; //Zapamiętaj uid operatu
            operat.Typ = typ.Value; //Zapamiętaj typ operatu
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
            if (!operat.Id.HasValue)
                throw new InvalidOperationException(
                    message: "Operat nie został odnaleziony w bazie danych Ośrodka: " + operat.IdZasobu);
            //Czy zawiera jakieś dokumenty w bazie danych?
            var dokumenty = OperatDb.SzukajDokumentów(operat.Id.Value);
            if (dokumenty.Any()) return false; //Operat posiada już dokumenty
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
            foreach (var dokument in operat.Dokumenty) ZapiszDokumentOperatu(dokument);
        }

        void ZapiszDokumentOperatu(DokumentOperatu dokument)
        {
            if (dokument.Id.HasValue) return; //Pomiń operaty, które posiadają już dokumenty w bazie
            //Odczytaj plik z dysku
            var plik = dokument.WczytajPlik();
            var plikId = ZapiszPlik(dokument, plik);
            dokument.Rozdzielczość = plik.OdczytajRozdzielczość();
            var dokumentId = ZapiszDokument(dokument);
        }

        int ZapiszPlik(DokumentOperatu dokument, byte[] blob)
        {
            var plikId = PlikDb.DodajPlik(blob);
            dokument.PlikId = plikId; //Zapamiętaj id dodanego pliku
            return plikId;
        }

        int ZapiszDokument(DokumentOperatu dokument)
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
                var dokument = new DokumentOperatu { Id = dokumentId };
                operat.Dodaj(dokument);
            }
            return true;
        }

    }
}
