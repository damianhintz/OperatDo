using System;
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
    }
}
