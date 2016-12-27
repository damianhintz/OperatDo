using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class OperatTest
    {
        [TestMethod]
        public void Operat_ShouldBeEmpty()
        {
            var operat = new Operat();
            operat.Id.ShouldBeNull();
            operat.Typ.ShouldBeNull();
            operat.Dokumenty.ShouldBeNull();
            operat.IdZasobu.ShouldBeNull();
            operat.Pliki.ShouldBeEmpty();
            operat.Rozmiar.Bajty.ShouldBe(0);
        }

        [TestMethod]
        public void Operat_ShouldAddOneDokument()
        {
            var operat = new Operat();
            var dokument = new PlikOperatu();
            operat.Dodaj(dokument);
            dokument.Operat.ShouldBeSameAs(operat);
            operat.Pliki.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void Operat_ShouldNotAddEmptyDokument()
        {
            var operat = new Operat();
            Should.Throw<ArgumentNullException>(() =>
            {
                operat.Dodaj(null);
            });
        }

        [TestMethod]
        public void Operat_ShouldNotHaveFolder()
        {
            var operat = new Operat();
            Should.Throw<InvalidOperationException>(() =>
            {
                var folder = operat.Folder;
            });
        }
    }
}
