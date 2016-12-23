using System;
using System.Collections.Generic;
using System.Linq;

namespace OśrodekDomena
{
    /// <summary>
    /// Dokument składający się na operat.
    /// </summary>
    public class DokumentOperatu
    {
        public int? Id { get; set; }
        public Operat Operat { get; set; }
        public int? PlikId { get; set; }
        public string Plik { get; set; }
        public Rozdzielczość Rozdzielczość { get; set; }
    }
}
