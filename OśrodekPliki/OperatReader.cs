using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using OśrodekDomena;
using OśrodekFirebird;

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
        /// Lista znalezionych i wczytanych plików.
        /// </summary>
        public IEnumerable<string> Pliki => _files;
        string[] _files = new string[0];

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
        /// <param name="searchPattern"></param>
        public void Wczytaj(string folder, string searchPattern = "*.jpg")
        {
            _files = Directory.GetFiles(path: folder,
                searchPattern: searchPattern,
                searchOption: SearchOption.AllDirectories);
            var operaty = _files.GroupBy(path => new FileInfo(path).Directory.Name);
            //P.2801.rok.nr
            foreach (var operatDokumenty in operaty)
            {
                var idZasobu = operatDokumenty.Key;
                var operat = new Operat { IdZasobu = idZasobu };
                foreach (var fileName in operatDokumenty)
                {
                    var dokument = new DokumentOperatu { Plik = fileName };
                    operat.Dodaj(dokument);
                }
                _operaty.Dodaj(operat);
            }
        }
    }
}
