using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OśrodekPliki.Rozszerzenia
{
    /// <summary>
    /// Rozszerzenia czytnika operatów.
    /// </summary>
    public static class ReaderRozszerzenia
    {
        public static IEnumerable<FileInfo> PlikiOperatów(this OperatReader reader)
        {
            return reader.Pliki
                .Where(f => f.Name.ToLower().EndsWith(".jpg"));
        }

        public static IEnumerable<FileInfo> DodatkowePliki(this OperatReader reader)
        {
            return reader.Pliki
                .Where(f => !f.Name.ToLower().EndsWith(".jpg"));
        }

        /// <summary>
        /// Lista pustych plików.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static IEnumerable<FileInfo> PustePliki(this OperatReader reader)
        {
            return reader.PlikiOperatów()
                .Where(p => p.Length == 0);
        }

        public static IEnumerable<string> PusteOperaty(this OperatReader reader)
        {
            var foldery = reader.Pliki.GroupBy(f => f.Directory.FullName);
            var emptyOperats = new List<string>();
            foreach (var folderGroup in foldery)
            {
                var name = folderGroup.Key;
                var operatPliki = folderGroup.Where(f => f.Name.ToLower().EndsWith(".jpg"));
                if (!operatPliki.Any()) emptyOperats.Add(name);
            }
            return emptyOperats;
        }
    }
}
