using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekTesty.Rozszerzenia;
using Shouldly;
using OśrodekDomena;

namespace OśrodekTesty
{
    [TestClass]
    public class OśrodekOperatDbTest
    {
        [TestMethod]
        public void OśrodekDb_ShouldBeEmpty()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            db.PoliczOperaty().ShouldBe(0);
            db.PoliczDokumenty().ShouldBe(0);
        }

        [TestMethod]
        public void OśrodekDb_ShouldNotFindDocuments()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            db.SzukajDokumentów(idOperatu: 1).ShouldBeEmpty();
            db.SzukajDokumentów(new int[] { 1 }).ShouldBe(0);
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindOneDocument()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "*.jpg", 300, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            db.SzukajDokumentów(new int[] { operatId }, dokumenty, operaty).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
        }

        [TestMethod]
        public void OśrodekDb_ShouldAddOneDocument()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            db.PoliczOperaty().ShouldBe(0);
            db.PoliczDokumenty().ShouldBe(0);
            var dpi = new Rozdzielczość(300);
            var id = db.DodajDokument(1, 'E', 4, "nazwa", dpi.Ppm, 1);
            id.ShouldBeGreaterThan(1);
            db.PoliczDokumenty().ShouldBe(1);
            char operatTyp = '0';
            int operatId = 0, plikId = 0, ppmX = 0, ppmY = 0;
            string nazwa = string.Empty;
            var count = db.SzukajDokumentu(id, ref operatTyp, ref operatId, ref plikId, ref ppmX, ref ppmY, ref nazwa);
            count.ShouldBe(1);
            operatTyp.ShouldBe('E');
            operatId.ShouldBe(1);
            plikId.ShouldBe(1);
            ppmX.ShouldBe(dpi.Ppm);
            ppmY.ShouldBe(dpi.Ppm);
            nazwa.ShouldBe("nazwa");
        }


        [TestMethod]
        public void OśrodekDb_ShouldAddObliczenia()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            db.PoliczOperaty().ShouldBe(0);
            db.PoliczDokumenty().ShouldBe(0);
            var dpi = new Rozdzielczość(200);
            var id = db.DodajDokument(1, 'E', 4, "Obliczenia", dpi.Ppm, 1);
            id.ShouldBeGreaterThan(1);
            db.PoliczDokumenty().ShouldBe(1);
            char operatTyp = '0';
            int operatId = 0, plikId = 0, ppmX = 0, ppmY = 0;
            string nazwa = string.Empty;
            var count = db.SzukajDokumentu(id, 
                ref operatTyp, ref operatId, ref plikId, ref ppmX, ref ppmY, ref nazwa);
            count.ShouldBe(1);
            operatTyp.ShouldBe('E');
            operatId.ShouldBe(1);
            plikId.ShouldBe(1);
            ppmX.ShouldBe(dpi.Ppm);
            ppmY.ShouldBe(dpi.Ppm);
            nazwa.ShouldBe("Obliczenia");
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindObliczenia()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Obliczenia", 300, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId }, 
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(72);
            nazwy.ShouldContain("01 Obliczenia");
            pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindSzkice()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId1 = db.DodajDokument(operatId, 'E', 4, "01 Szkice podstawowe", 300, 1, 1);
            var dokumentId2 = db.DodajDokument(operatId, 'E', 4, "02 Szkice polowe", 300, 2, 2);
            db.PoliczDokumenty().ShouldBe(2);
            db.SzukajDokumentów(operatId).Count().ShouldBe(2);
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery)
                .ShouldBe(2);
            dokumenty.Count().ShouldBe(2);
            dokumenty.ShouldContain(dokumentId1);
            dokumenty.ShouldContain(dokumentId2);
            operaty.Count().ShouldBe(2);
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            numery.ShouldContain(2);
            rodzaje.Distinct().ShouldContain(66);
            nazwy.ShouldContain("01 Szkice podstawowe");
            nazwy.ShouldContain("02 Szkice polowe");
            pliki.Count().ShouldBe(2);
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindWykazWsp()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Wykaz współrzędnych", 300, 1, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(67);
            nazwy.ShouldContain("01 Wykaz współrzędnych");
            pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindProtokół()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Protokół", 300, 1, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(68);
            nazwy.ShouldContain("01 Protokół");
            pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindOpisyTopo()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Opisy topograficzne", 300, 1, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(69);
            nazwy.ShouldContain("01 Opisy topograficzne");
            pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindSprawozdanieTechniczne()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Sprawozdanie techniczne", 300, 1, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(70);
            nazwy.ShouldContain("01 Sprawozdanie techniczne");
            pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindMapy()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Mapy", 300, 1, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(71);
            nazwy.ShouldContain("01 Mapy");
            pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void OśrodekDb_ShouldFindInne()
        {
            var db = OśrodekDbRozszerzenia.SampleDb();
            var operatId = db.DodajOperat("P.2801.2016.1");
            db.PoliczOperaty().ShouldBe(1);
            var dokumentId = db.DodajDokument(operatId, 'E', 4, "01 Inne", 300, 1, 1);
            db.PoliczDokumenty().ShouldBe(1);
            db.SzukajDokumentów(operatId).ShouldHaveSingleItem();
            var dokumenty = new List<int>();
            var operaty = new List<int>();
            var pliki = new List<int>();
            var nazwy = new List<string>();
            var rodzaje = new List<int>();
            var numery = new List<int>();
            db.SzukajDokumentów(new int[] { operatId },
                dokumenty, operaty, pliki, nazwy, rodzaje, numery).ShouldBe(1);
            dokumenty.ShouldHaveSingleItem();
            dokumenty.ShouldContain(dokumentId);
            operaty.ShouldHaveSingleItem();
            operaty.ShouldContain(operatId);
            numery.ShouldContain(1);
            rodzaje.ShouldContain(73);
            nazwy.ShouldContain("01 Inne");
            pliki.ShouldHaveSingleItem();
        }
    }
}
