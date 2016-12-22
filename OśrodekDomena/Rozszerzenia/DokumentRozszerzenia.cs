using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging; //PresentationCore

namespace OśrodekDomena.Rozszerzenia
{
    public static class DokumentRozszerzenia
    {
        public static byte[] WczytajPlik(this string fileName)
        {
            return File.ReadAllBytes(fileName);
        }

        public static int OdczytajRozdzielczość(this byte[] bytes)
        {
            var ms = new MemoryStream(bytes);
            var bitmap = BitmapFrame.Create(ms);
            var dpi = (int)bitmap.DpiX;
            var ppm = (dpi * 100) / 2.54;
            return (int)ppm;
        }
    }
}
