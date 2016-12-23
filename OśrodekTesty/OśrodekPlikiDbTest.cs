using System;
using System.Linq;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekFirebird;
using OśrodekTesty.Rozszerzenia;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class OśrodekPlikiDbTest
    {
        [TestMethod]
        public void OśrodekPlikiDb_ShouldBeEmpty()
        {
            var db = OśrodekDbRozszerzenia.SamplePlikiDb();
            db.PoliczPliki().ShouldBe(0);
        }

        [TestMethod]
        public void OśrodekPlikiDb_ShouldAddOneFile()
        {
            var db = OśrodekDbRozszerzenia.SamplePlikiDb();
            db.PoliczPliki().ShouldBe(0);
            var plik = new byte[1] { 64 };
            var plikId = db.DodajPlik(plik);
            db.PoliczPliki().ShouldBe(1);
            var treść = new byte[0];
            var count = db.SzukajPliku(plikId, ref treść);
            count.ShouldBe(1);
            treść.ShouldHaveSingleItem();
            treść.First().ShouldBe((byte)64);
        }
    }
}
