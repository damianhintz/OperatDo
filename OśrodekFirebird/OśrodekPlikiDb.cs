using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace OśrodekFirebird
{
    /// <summary>
    /// Baza danych skanów dokumentów w Ośrodku.
    /// </summary>
    public class OśrodekPlikiDb : OśrodekDb
    {
        public OśrodekPlikiDb(OśrodekConfig config) : base(config) { }

        void UpdateBlob(int uid, byte[] data)
        {
            var cmd = new FbCommand("update fbdok set tresc=@data where uid=" + uid, _connection);
            cmd.Parameters.Add("@data", data);
            cmd.ExecuteNonQuery();
        }

        public int SzukajOperatu(string idZasobu)
        {
            var @params = idZasobu.Split('.'); //P.2801.rok.nr
            if (@params.Length != 4) return -1;
            var c1 = @params[0][0];
            var c2 = @params[1];
            var c3 = @params[2];
            var c4 = @params[3].Split('_').First();
            var cmdOperaty = _connection.CreateCommand();
            cmdOperaty.CommandText = "SELECT UID FROM OPERATY where C1=@c1 and C2=@c2 and C3=@c3 and C4=@c4;";
            var c1Param = new FbParameter("@c1", FbDbType.Char);
            c1Param.Value = c1; //P
            var c2Param = new FbParameter("@c2", FbDbType.VarChar);
            c2Param.Value = c2; //P.2801
            var c3Param = new FbParameter("@c3", FbDbType.Integer);
            c3Param.Value = int.Parse(c3); //P.2801.Rok
            var c4Param = new FbParameter("@c4", FbDbType.Integer);
            c4Param.Value = int.Parse(c4); //P.2801.Rok.Numer w roku
            cmdOperaty.Parameters.Add(c1Param);
            cmdOperaty.Parameters.Add(c2Param);
            cmdOperaty.Parameters.Add(c3Param);
            cmdOperaty.Parameters.Add(c4Param);
            var uidOperatów = new List<int>();
            var reader = cmdOperaty.ExecuteReader();
            while (reader.Read()) uidOperatów.Add(reader.GetInt32(0));
            return uidOperatów.Count == 1 ? uidOperatów.First() : -1;
        }

        public IEnumerable<int> SzukajDokumentów(params int[] uidOperatów)
        {
            var cmdDokumenty = _connection.CreateCommand();
            var ide = string.Join(",", uidOperatów);
            cmdDokumenty.CommandText = "SELECT UID, ID_OPE, ID_BLOB FROM OPERDOK WHERE ID_OPE IN (" + ide + ");";
            //var uidParam = new FbParameter("@uidOperatu", FbDbType.Array);
            //uidParam.Value = uidOperatów;
            //cmdDokumenty.Parameters.Add(uidParam);
            var uidDokumentów = new List<int>();
            var reader = cmdDokumenty.ExecuteReader();
            while (reader.Read())
            {
                var uid = reader.GetInt32(0);
                //var id_ope = reader.GetInt32(1);
                //var id_blob = reader.GetInt32(2);
                uidDokumentów.Add(uid);
            }
            return uidDokumentów;
        }

    }
}
