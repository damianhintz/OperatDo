using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace OśrodekFirebird
{
    /// <summary>
    /// Baza danych operatów Ośrodka.
    /// </summary>
    public class OśrodekOperatDb : OśrodekDb
    {
        public OśrodekOperatDb(OśrodekConfig config) : base(config) { }
        
        public bool SzukajOperatu(string idZasobu, out int? idOperatu, out char? typOperatu)
        {
            var @params = idZasobu.Split('.'); //P.2801.rok.nr
            idOperatu = null;
            typOperatu = null;
            if (@params.Length != 4) return false; //Nieprawidłowy format id zasobu
            var c1 = @params[0][0];
            var c2 = @params[1];
            var c3 = @params[2];
            var c4 = @params[3].Split('_').First();
            var cmdOperaty = _connection.CreateCommand();
            cmdOperaty.CommandText = "SELECT UID,TYP FROM OPERATY where C1=@c1 and C2=@c2 and C3=@c3 and C4=@c4;";
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
            var typOperatów = new List<char>();
            var reader = cmdOperaty.ExecuteReader();
            while (reader.Read())
            {
                uidOperatów.Add(reader.GetInt32(0));
                typOperatów.Add(reader.GetString(1).First());
            }
            if (uidOperatów.Count != 1) return false; //Brak operatu lub jest ich więcej niż jeden
            idOperatu = uidOperatów.First();
            typOperatu = typOperatów.First();
            return true;
        }

        public IEnumerable<int> SzukajDokumentów(int idOperatu)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "SELECT UID FROM OPERDOK WHERE ID_OPE=@uidOperatu;";
            var uidParam = new FbParameter("@uidOperatu", FbDbType.Integer);
            uidParam.Value = idOperatu;
            cmd.Parameters.Add(uidParam);
            var dokumenty = new List<int>();
            var reader = cmd.ExecuteReader();
            while (reader.Read()) dokumenty.Add(reader.GetInt32(0));
            return dokumenty;
        }

        public int SzukajDokumentów(int[] idOperatów,
            List<int> dokumenty = null, List<int> operaty = null, List<int> bloby = null)
        {
            var idArray = string.Join(",", idOperatów);
            var cmd = _connection.CreateCommand();
            cmd.CommandText =
                "SELECT UID, ID_OPE, ID_BLOB FROM OPERDOK WHERE ID_OPE IN (" + idArray + ");";
            var reader = cmd.ExecuteReader();
            var count = 0;
            while (reader.Read())
            {
                if (dokumenty != null)
                {
                    var uid = reader.GetInt32(0); //UID
                    dokumenty.Add(uid);
                }
                if (operaty != null)
                {
                    var id_ope = reader.GetInt32(1); //ID_OPE
                    operaty.Add(id_ope);
                }
                if (bloby != null)
                {
                    var id_blob = reader.GetInt32(2); //_ID_BLOB
                    bloby.Add(id_blob);
                }
                count++;
            }
            return count;
        }

        void Szukaj(string id)
        {
            DataTable userTables = _connection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
            var rows = userTables.Rows;
            var cols = userTables.Columns;
            foreach (DataColumn t in cols) Console.WriteLine(string.Join(",", t.ColumnName));
            //foreach (DataRow t in rows) Console.WriteLine(string.Join(",", t.ItemArray));
            Console.WriteLine(_connection.Database);
            Console.WriteLine(_connection.ServerVersion);
            FbCommand cmd = _connection.CreateCommand();
            //--insert into users (uid,usr_name) values (next value for usersg,'test1') returning uid;
            cmd.CommandText = "SELECT a.UID FROM OPERATY a where C1=@c1 and C2=@c2 and C3=@c3 and C4=@c4;";
            FbParameter c1 = new FbParameter("@c1", FbDbType.Char);
            //c1.Direction = ParameterDirection.Output;
        }
    }
}
