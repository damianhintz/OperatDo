using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Media.Imaging; //PresentationCore

namespace OśrodekDomena.Rozszerzenia
{
    public static class DokumentRozszerzenia
    {
        public static byte[] WczytajPlik(this DokumentOperatu dokument)
        {
            return File.ReadAllBytes(dokument.Plik);
        }

        /// <summary>
        /// Zwraca rozdzielczość obrazu.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Rozdzielczość OdczytajRozdzielczość(this byte[] bytes)
        {
            var ms = new MemoryStream(bytes);
            var bitmap = BitmapFrame.Create(ms);
            var dpi = (int)bitmap.DpiX;
            return new Rozdzielczość(dpi);
        }
    }
}
