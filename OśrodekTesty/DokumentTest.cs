using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class DokumentTest
    {
        [TestMethod]
        public void Dokument_ShouldBeEmpty()
        {
            var dokument = new DokumentOperatu();
            dokument.Id.ShouldBeNull();
            dokument.PlikId.ShouldBeNull();
            dokument.Plik.ShouldBeNull();
            dokument.Rozdzielczość.ShouldBe(0);
        }
    }
}
