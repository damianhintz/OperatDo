using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OśrodekFirebird
{
    /// <summary>
    /// Baza danych skanów dokumentów w Ośrodku.
    /// </summary>
    public class OśrodekPlikiDb : OśrodekDb
    {
        public OśrodekPlikiDb(OśrodekConfig config) : base(config) { }

        public int SzukajPliku(int plikId, ref byte[] treść)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText =
                "SELECT UID,TRESC FROM fbdok WHERE UID = " + plikId;
            var reader = cmd.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                treść = (byte[])reader["TRESC"];
                count++;
            }
            cmd.Dispose();
            return count;
        }
    }
}
