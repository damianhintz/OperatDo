using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OśrodekDomena;
using OśrodekPliki.Rozszerzenia;

namespace OśrodekPliki
{
    /// <summary>
    /// Czytnik zeskanowanych operatów.
    /// </summary>
    public class OperatReader
    {
        /// <summary>
        /// Lista znalezionych i wczytanych operatów.
        /// </summary>
        public RepozytoriumOperatów Operaty => _operaty;
        RepozytoriumOperatów _operaty;

        /// <summary>
        /// Wszystkie znalezione pliki w wybranym katalogu.
        /// </summary>
        public IEnumerable<FileInfo> Pliki => _files;
        List<FileInfo> _files;
        List<DirectoryInfo> _folders;

        public OperatReader(RepozytoriumOperatów operaty)
        {
            if (operaty == null)
                throw new ArgumentNullException(
                    paramName: "operaty",
                    message: "Operaty jest null");
            _operaty = operaty;
        }

        /// <summary>
        /// Wczytaj zeskanowane operaty z dysku.
        /// </summary>
        /// <param name="folder"></param>
        public void Wczytaj(string folder)
        {
            var allFiles = Directory.GetFiles(path: folder,
                searchPattern: "*.*",
                searchOption: SearchOption.AllDirectories);
            _files = allFiles.Select(f => new FileInfo(f)).ToList();
            var operatFiles = this.PlikiOperatów();
            var operaty = operatFiles.GroupBy(info => info.Directory.FullName);
            var wczytaneOperaty = new RepozytoriumOperatów();
            foreach (var operatPliki in operaty)
            {
                var operatPath = operatPliki.Key; //P.2801.rok.nr
                var operat = DodajOperat(operatPath, operatPliki);
                wczytaneOperaty.Dodaj(operat);
            }
            var powtórzoneOperaty =
                from op in wczytaneOperaty
                where _operaty.Zawiera(op.IdZasobu)
                select op.IdZasobu;
            if (powtórzoneOperaty.Any())
                throw new InvalidOperationException(
                    message: "Repozytorium zawiera już operat[y]: " + 
                    string.Join(", ", powtórzoneOperaty));
            foreach (var operat in wczytaneOperaty)
                _operaty.Dodaj(operat);
        }

        Operat DodajOperat(string folder, IEnumerable<FileInfo> pliki)
        {
            var name = Path.GetFileName(folder); //P.2801.1979.322_T1
            var idZasobu = IdZasobu.Parse(name.Split('_').First()); //Wyrzuć tom z nazwy folderu
            var operat = new Operat { IdZasobu = idZasobu.Id };
            foreach (var fileInfo in pliki)
            {
                var fileName = fileInfo.FullName;
                var dokument = new PlikOperatu { Plik = fileName };
                dokument.Rozmiar = RozmiarPliku.Wczytaj(fileName);
                operat.Dodaj(dokument);
            }
            //_operaty.Dodaj(operat);
            return operat;
        }
    }
}
