using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OśrodekDomena
{
    /// <summary>
    /// Rozmiar pliku na dysku.
    /// </summary>
    public class RozmiarPliku
    {
        public long GigaBajty => MegaBajty / 1024;
        public long MegaBajty => KiloBajty / 1024;
        public long KiloBajty => Bajty / 1024;
        public long Bajty { get; set; }

        public RozmiarPliku(long bajty)
        {
            Bajty = bajty;
        }

        public static RozmiarPliku Wczytaj(string fileName)
        {
            var info = new FileInfo(fileName);
            var rozmiar = new RozmiarPliku(info.Length);
            return rozmiar;
        }
    }
}
