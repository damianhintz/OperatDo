using System;
using System.Collections.Generic;
using System.Linq;

namespace OśrodekDomena
{
    /// <summary>
    /// Plik składający się na operat.
    /// </summary>
    public class PlikOperatu
    {
        public int? Id { get; set; }
        public Operat Operat { get; set; }
        public int? PlikId { get; set; }
        public string Plik { get; set; }
        public RozmiarPliku Rozmiar { get; set; }
        public Rozdzielczość Rozdzielczość { get; set; }
    }
}
