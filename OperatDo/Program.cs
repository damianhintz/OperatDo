using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OśrodekDomena;
using OśrodekPliki;
using OśrodekFirebird;

namespace OperatDo
{
    class Program
    {
        static readonly string _searchPattern = "*.jpg";
        RepozytoriumOperatów _operaty = new RepozytoriumOperatów();
        OśrodekConfig _dbConfig = OśrodekConfig.Wczytaj(fileName: @"Samples\Osrodek.json");
        OśrodekConfig _blobConfig = OśrodekConfig.Wczytaj(fileName: @"Samples\OsrodekBlob.json");

        static void Main(string[] args)
        {
            var program = new Program();
            Console.WriteLine("OśrodekOperaty v1.0-alfa, 20 grudnia 2016");
            Console.WriteLine("Importuj zeskanowane operaty do bazy Ośrodka");
            program.WczytajPliki(_searchPattern);
            program.WczytajDokumenty();
            Console.WriteLine("Koniec.");
            Console.Read();
        }

        void WczytajPliki(string searchPattern)
        {
            //var folder = @"s:\2903_skanowanie Bartoszyce\skanowanie\2016\";
            var folder = @"c:\flamerobin-0.9.2-1-x64\P.2801.2016.121_T1\";
            var reader = new OperatReader(_operaty);
            reader.Wczytaj(folder);
            Console.WriteLine("Folder: {0}", folder);
            Console.WriteLine("Pliki ({1}): {0}", reader.Pliki.Count(), searchPattern);
            Console.WriteLine("Operaty (P.2801.*.*): {0}", _operaty.Operaty.Count());
        }

        public void WczytajDokumenty()
        {
            var db = new OśrodekOperatDb(_dbConfig);
            var blob = new OśrodekPlikiDb(_blobConfig);
            var writer = new OperatWriter
            {
                OperatDb = db,
                PlikDb = blob
            };
            Console.WriteLine("Odczytywanie operatów z bazy danych Ośrodka...");
            var result = writer.WczytajOperat(null);
            if (!result)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} nieznalezione operaty na {1}",
                    _operaty.NieznalezioneOperaty.Count(), _operaty.Count());
                Console.WriteLine("Import przerwany, sprawdź brakujące operaty OśrodekOperaty.log");
                Console.ResetColor();
                return;
            }
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0} znalezione na {1} operat[y]",
                _operaty.ZnalezioneOperaty.Count(), _operaty.Count());
            Console.ResetColor();
            Console.WriteLine("Odczytywanie dokumentów z bazy danych Ośrodka...");
            //Console.WriteLine("Dodawanie plików do bazy danych (Ośrodek.fdr)...");
            //Console.WriteLine("Dodawanie dokumentów do bazy danych (Ośrodek.fdb)...");
        }
    }
}
