using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekTesty.Rozszerzenia;
using Shouldly;

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
            var dokumentId = db.DodajDokument(operatId, 'E', "*.jpg", 300, 1);
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
            var id = db.DodajDokument(1, 'E', "", 300, 1);
            id.ShouldBeGreaterThan(1);
            db.PoliczDokumenty().ShouldBe(1);
        }
    }
}
