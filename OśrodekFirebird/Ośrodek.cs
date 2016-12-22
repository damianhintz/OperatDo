using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OśrodekFirebird
{
    public class Ośrodek
    {
        public OśrodekConfig Operaty { get; set; }
        public OśrodekConfig Dokumenty { get; set; }
        public OśrodekConfig Pliki { get; set; }
        public OśrodekConfig Użytkownicy { get; set; }
    }
}
