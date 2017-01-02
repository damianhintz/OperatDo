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
            var operaty = operatFiles.GroupBy(info => info.Directory.Name);
            //P.2801.rok.nr
            foreach (var operatDokumenty in operaty)
            {
                var idZasobu = operatDokumenty.Key;
                var operat = new Operat { IdZasobu = idZasobu };
                foreach (var fileInfo in operatDokumenty)
                {
                    var fileName = fileInfo.FullName;
                    var dokument = new PlikOperatu { Plik = fileName };
                    dokument.Rozmiar = RozmiarPliku.Wczytaj(fileName);
                    operat.Dodaj(dokument);
                }
                _operaty.Dodaj(operat);
            }
            var foldery = _files.GroupBy(f => f.Directory.Name);
            var emptyOperats = new List<string>();
            foreach(var folderGroup in foldery)
            {
                var name = folderGroup.Key;
                var operatPliki = folderGroup.Where(f => f.Name.ToLower().EndsWith(".jpg"));
                if (!operatPliki.Any()) emptyOperats.Add(name);
            }
        }
    }
}
