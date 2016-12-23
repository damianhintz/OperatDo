using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OśrodekDomena
{
    /// <summary>
    /// Rozdzielczość obrazu.
    /// </summary>
    public class Rozdzielczość
    {
        /// <summary>
        /// Rozdzielczość w punktach na metr.
        /// </summary>
        public int Ppm { get { return (int)((Dpi * 100) / 2.54); } }

        /// <summary>
        /// Rozdzielczość w punktach na cal.
        /// </summary>
        public int Dpi { get; set; }
        
        public Rozdzielczość(int dpi) { Dpi = dpi; }
    }
}
