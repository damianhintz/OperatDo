using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FirebirdSql.Data.FirebirdClient;

namespace OśrodekFirebird
{
    public abstract class OśrodekDb : IDisposable
    {
        /// <summary>
        /// Konfiguracja połączenia.
        /// </summary>
        public OśrodekConfig Config { get; private set; }

        /// <summary>
        /// Połączenie do bazy danych.
        /// </summary>
        public FbConnection Connection => _connection;
        protected FbConnection _connection;

        /// <summary>
        /// Bieżąca transkacja;
        /// </summary>
        public FbTransaction Transakcja => _t;
        private FbTransaction _t;

        protected Dictionary<string, int> _rodzaje;

        public OśrodekDb(OśrodekConfig config)
        {
            Connect(config);
        }

        void Connect(OśrodekConfig config)
        {
            Config = config;
            var connectionBuilder = new FbConnectionStringBuilder();
            connectionBuilder.DataSource = config.Host;
            connectionBuilder.Database = config.Path;
            connectionBuilder.UserID = config.User;
            connectionBuilder.Password = config.Password;
            connectionBuilder.Charset = config.Charset;
            connectionBuilder.Pooling = false;
            _connection = new FbConnection(connectionBuilder.ToString());
            _connection.Open();
        }

        public int PoliczOperaty() { return CountTable("operaty"); }
        public int PoliczDokumenty() { return CountTable("operdok"); }
        public int PoliczPliki() { return CountTable("fbdok"); }

        int CountTable(string table)
        {
            var cmd = _connection.CreateCommand();
            cmd.CommandText = "select count(*) from " + table;
            return (int)cmd.ExecuteScalar();
        }

        public int DodajOperat(string idZasobu, char typ = 'E', int osoba = 31)
        {
            var @params = idZasobu.Split('.'); //P.2801.rok.nr
            var c1 = @params[0].First();
            var c2 = @params[1];
            var c3 = int.Parse(@params[2]);
            var c4 = int.Parse(@params[3]);
            return DodajOperat(typ, c1, c2, c3, c4, osoba);
        }

        /// <summary>
        /// Dodaj operat do bazy danych Ośrodka.
        /// </summary>
        /// <param name="typ"></param>
        /// <param name="c1"></param>
        /// <param name="c2"></param>
        /// <param name="c3"></param>
        /// <param name="c4"></param>
        /// <returns></returns>
        public int DodajOperat(char typ, char c1, string c2, int c3, int c4, int osoba = 31)
        {
            var cmd = new FbCommand(
                "insert into operaty (uid,typ,c1,c2,c3,c4,dtw,osow) values " +
                "(next value for operatyg,@typ,@c1,@c2,@c3,@c4,@data,@osoba) returning uid",
                _connection);
            var typParam = new FbParameter("@typ", FbDbType.Char);
            typParam.Value = typ; //'E'
            var c1Param = new FbParameter("@c1", FbDbType.Char);
            c1Param.Value = c1; //P
            var c2Param = new FbParameter("@c2", FbDbType.VarChar);
            c2Param.Value = c2; //P.2801
            var c3Param = new FbParameter("@c3", FbDbType.Integer);
            c3Param.Value = c3; //P.2801.Rok
            var c4Param = new FbParameter("@c4", FbDbType.Integer);
            c4Param.Value = c4; //P.2801.Rok.Numer w roku
            var dataParam = new FbParameter("@data", FbDbType.TimeStamp);
            dataParam.Value = DateTime.Now;
            var osobaParam = new FbParameter("@osoba", FbDbType.Integer);
            osobaParam.Value = osoba; //sysdba
            cmd.Parameters.Add(typParam);
            cmd.Parameters.Add(c1Param);
            cmd.Parameters.Add(c2Param);
            cmd.Parameters.Add(c3Param);
            cmd.Parameters.Add(c4Param);
            cmd.Parameters.Add(dataParam);
            cmd.Parameters.Add(osobaParam);
            return (int)cmd.ExecuteScalar();
        }

        /// <summary>
        /// Dodaj dokument do bazy danych Ośrodka.
        /// </summary>
        /// <param name="operatId"></param>
        /// <param name="operatTyp"></param>
        /// <param name="plik"></param>
        /// <param name="rozdzielczość"></param>
        /// <param name="plikId"></param>
        /// <returns></returns>
        public int DodajDokument(int operatId, char operatTyp,
            int typPliku, string plik, int rozdzielczość, int plikId,
            int lp = 1, int osoba = 31)
        {
            var cmd = new FbCommand(
                "insert into operdok (uid,typ,id_ope,dokument,typ_dok,xppm,yppm,kompresja,id_blob,dtw,osow,nr_dok,naz_dok) values " +
                "(next value for operdokg,@typ,@operat,@plik,@typPliku,@dpp,@dpp,@kompresja,@blob,@data,@osoba,@lp,@nazwa) returning uid;",
                _connection);
            cmd.Transaction = Transakcja;
            var typParam = new FbParameter("@typ", FbDbType.Char, 1);
            typParam.Value = operatTyp; //Typ taki sam jak typ operatu
            var operatParam = new FbParameter("@operat", FbDbType.Integer);
            operatParam.Value = operatId;
            var plikParam = new FbParameter("@plik", FbDbType.VarChar, 30);
            plikParam.Value = plik.Length > 30 ? plik.Substring(0, 30) : plik; //30 znaków
            var typPlikuParam = new FbParameter("@typPliku", FbDbType.SmallInt);
            typPlikuParam.Value = typPliku; //Typ dokumentu
            var rozdzielczośćParam = new FbParameter("@dpp", FbDbType.Integer);
            rozdzielczośćParam.Value = rozdzielczość;
            var kompresjaParam = new FbParameter("@kompresja", FbDbType.SmallInt);
            kompresjaParam.Value = 0; //Brak kompresji
            var blobParam = new FbParameter("@blob", FbDbType.Integer);
            blobParam.Value = plikId;
            var dataParam = new FbParameter("@data", FbDbType.TimeStamp);
            dataParam.Value = DateTime.Now;
            var osobaParam = new FbParameter("@osoba", FbDbType.Integer);
            osobaParam.Value = osoba; //sysdba
            var lpParam = new FbParameter("@lp", FbDbType.Integer);
            lpParam.Value = lp;
            var nazwaParam = new FbParameter("@nazwa", FbDbType.Integer);
            nazwaParam.Value = UstalTypDokumentu(plik); //PZG_SLOWNIK
            cmd.Parameters.Add(typParam);
            cmd.Parameters.Add(operatParam);
            cmd.Parameters.Add(plikParam);
            cmd.Parameters.Add(typPlikuParam);
            cmd.Parameters.Add(rozdzielczośćParam);
            cmd.Parameters.Add(kompresjaParam);
            cmd.Parameters.Add(blobParam);
            cmd.Parameters.Add(dataParam);
            cmd.Parameters.Add(osobaParam);
            cmd.Parameters.Add(lpParam);
            cmd.Parameters.Add(nazwaParam);
            return (int)cmd.ExecuteScalar();
        }

        int UstalTypDokumentu(string name)
        {
            if (string.IsNullOrEmpty(name)) return _rodzaje["inny"];
            name = name.ToLower();
            if (name.Contains("obliczenia") || name.EndsWith("_o"))
                return _rodzaje["dziennikPomiarowy"]; //0
            if (name.Contains("szkice") || name.EndsWith("_s") || name.EndsWith("_sp"))
                return _rodzaje["szkicPolowyZbSzkicow"]; //1
            if (name.Contains("wykaz wsp") || name.EndsWith("_w"))
                return _rodzaje["wykazWspZbWykazowWsp"]; //2
            if (name.Contains("protokół") || name.Contains("protokol") || 
                name.EndsWith("_pe") || name.EndsWith("_pkl") || name.EndsWith("_pk") || name.EndsWith("_pg") || name.EndsWith("_pi"))
                return _rodzaje["protokolZbProtokolow"]; //3
            if (name.Contains("opisy topograficzne") || name.EndsWith("_t"))
                return _rodzaje["opisTopoZbOpisowTopo"]; //4
            if (name.Contains("sprawozdanie techniczne") || name.EndsWith("_st"))
                return _rodzaje["sprawTechniczne"]; //5
            if (name.Contains("mapy") || name.EndsWith("_m"))
                return _rodzaje["mapa"]; //6
            return _rodzaje["inny"]; //21 inne
        }
        
        /// <summary>
        /// Dodaj plik do bazy danych.
        /// </summary>
        /// <param name="blob"></param>
        /// <returns></returns>
        public int DodajPlik(byte[] blob, int osoba = 31)
        {
            var cmd = new FbCommand(
                "insert into fbdok (uid,tresc,dtw,osow) values " +
                "(next value for fbdokg, @tresc, @data,@osoba) returning uid",
                _connection);
            cmd.Transaction = Transakcja;
            var treśćParam = new FbParameter("@tresc", FbDbType.Binary);
            treśćParam.Value = blob;
            var dataParam = new FbParameter("@data", FbDbType.TimeStamp);
            dataParam.Value = DateTime.Now;
            var osobaParam = new FbParameter("@osoba", FbDbType.Integer);
            osobaParam.Value = osoba; //sysdba
            cmd.Parameters.Add(treśćParam);
            cmd.Parameters.Add(dataParam);
            cmd.Parameters.Add(osobaParam);
            return (int)cmd.ExecuteScalar();
        }

        public int UsuńPlik(int plikId)
        {
            var cmd = new FbCommand(
                "delete from fbdok where uid = " + plikId,
                _connection);
            cmd.Transaction = Transakcja;
            return (int)cmd.ExecuteScalar();
        }

        public int UsuńDokument(int dokumentId)
        {
            var cmd = new FbCommand(
                "delete from operdok where uid = " + dokumentId,
                _connection);
            cmd.Transaction = Transakcja;
            return (int)cmd.ExecuteScalar();
        }

        public IEnumerable<string> ExecuteFile(string fileName)
        {
            var records = File.ReadAllLines(fileName, Encoding.GetEncoding(1250));
            var result = new List<string>();
            foreach (var sql in records)
            {
                if (string.IsNullOrWhiteSpace(sql)) continue;
                if (sql.StartsWith("--")) continue;
                var wynik = Execute(sql);
                result.Add(string.Format("{0}: {1}", wynik, sql));
            }
            return result;
        }

        int Execute(string sql)
        {
            var cmd = new FbCommand(sql, _connection);
            return cmd.ExecuteNonQuery();
        }

        public void RozpocznijTransakcję() { _t = _connection.BeginTransaction(); }

        public void ZakończTransakcję()
        {
            _t.Commit();
            _t = null;
        }

        public void WycofajTransakcję()
        {
            _t.Rollback();
            _t = null;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
