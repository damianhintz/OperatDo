using System;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekPliki;
using Shouldly;
using OśrodekDomena;

namespace OśrodekTesty
{
    [TestClass]
    public class OperatReaderTest
    {
        [TestMethod]
        public void OperatyReader_ShouldBeEmpty()
        {
            var operaty = new RepozytoriumOperatów();
            var reader = new OperatReader(operaty);
            reader.Operaty.ShouldBeSameAs(operaty);
            reader.Pliki.ShouldBeEmpty();
        }

        [TestMethod]
        public void OperatyReader_ShouldNotAllowEmptyRepozytorium()
        {
            Should.Throw<ArgumentNullException>(() =>
            {
                var reader = new OperatReader(null);
            });
        }

        [TestMethod]
        public void OperatyReader_ShouldRead3OperatsAnd8Files()
        {
            var operaty = new RepozytoriumOperatów();
            var reader = new OperatReader(operaty);
            reader.Wczytaj(folder: @"..\..\Samples\Pliki");
            reader.Operaty.Count().ShouldBe(3);
            reader.Pliki.Count().ShouldBe(8);
        }
    }
}
