using System;
using System.Collections.Generic;
using System.Linq;

namespace OśrodekDomena
{
    public class DokumentOperatu
    {
        public int? Id { get; set; }
        public int? PlikId { get; set; }
        public string Plik { get; set; }
        public int Rozdzielczość { get; set; }
    }
}
