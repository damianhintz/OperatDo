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
            operat.IdZasobu.ShouldBeNull();
            operat.Dokumenty.ShouldBeEmpty();
            operat.DokumentyDb.ShouldBeEmpty();
        }

        [TestMethod]
        public void Operat_ShouldAddOneDokument()
        {
            var operat = new Operat();
            var dokument = new DokumentOperatu();
            operat.Dodaj(dokument);
            operat.Dokumenty.ShouldHaveSingleItem();
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
    }
}
