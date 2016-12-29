using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OśrodekDomena.Rozszerzenia
{
    public static class OperatRozszerzenia
    {
        public static void ZanumerujPliki(this Operat operat)
        {
            var pliki = 
                from p in operat.Pliki
                orderby p.Plik.NumerPliku(), p.Plik.ZnakiPliku()
                select p;
            var lp = 1;
            foreach (var plik in pliki) plik.Numer = lp++;
        }

        static int NumerPliku(this string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            var cyfry = new string(name.Where(char.IsDigit).ToArray());
            if (!cyfry.Any()) return 0;
            return int.Parse(cyfry);
        }

        static string ZnakiPliku(this string fileName)
        {
            var name = Path.GetFileNameWithoutExtension(fileName);
            return new string(name.Where(char.IsLetter).ToArray());
        }
    }
}
