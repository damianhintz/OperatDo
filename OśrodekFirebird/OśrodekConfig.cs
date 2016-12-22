using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace OśrodekFirebird
{
    /// <summary>
    /// Konfiguracja połączenia do bazy danych Ośrodka.
    /// </summary>
    public class OśrodekConfig
    {
        public string Host { get; set; }
        public string Path { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Charset { get; set; }

        public static OśrodekConfig Wczytaj(string fileName)
        {
            var json = File.ReadAllText(fileName, Encoding.GetEncoding(1250));
            return JsonConvert.DeserializeObject<OśrodekConfig>(json);
        }
    }
}
