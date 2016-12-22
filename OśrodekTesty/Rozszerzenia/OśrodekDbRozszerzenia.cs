using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using OśrodekFirebird;

namespace OśrodekTesty.Rozszerzenia
{
    static class OśrodekDbRozszerzenia
    {
        public static OśrodekOperatDb SampleDb(bool empty = true)
        {
            var db = new OśrodekOperatDb(SampleConfig());
            if (empty) db.ClearData();
            return db;
        }
        
        static IEnumerable<string> ClearData(this OśrodekOperatDb db)
        {
            return db.ExecuteFile(@"..\..\Samples\Osrodek_Delete.sql");
        }

        static OśrodekConfig SampleConfig()
        {
            return new OśrodekConfig
            {
                Host = "localhost",
                Path = Path.Combine(Environment.CurrentDirectory, @"..\..\Samples\Osrodek.fdb"),
                User = "SYSDBA",
                Password = "masterkey",
                Charset = "WIN1250"
            };
        }
        
        public static OśrodekPlikiDb SamplePlikiDb(bool empty = true)
        {
            var db = new OśrodekPlikiDb(SampleBlobConfig());
            if (empty) db.ClearData();
            return db;
        }
        
        static IEnumerable<string> ClearData(this OśrodekPlikiDb db)
        {
            return db.ExecuteFile(@"..\..\Samples\OsrodekPliki_Delete.sql");
        }

        static OśrodekConfig SampleBlobConfig()
        {
            return new OśrodekConfig
            {
                Host = "localhost",
                Path = Path.Combine(Environment.CurrentDirectory, @"..\..\Samples\OsrodekBlob.fdb"),
                User = "SYSDBA",
                Password = "masterkey",
                Charset = "WIN1250"
            };
        }
    }
}
