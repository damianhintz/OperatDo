using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using OśrodekDomena.Rozszerzenia;
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
            operat.DokumentyId.ShouldBeNull();
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

        [TestMethod]
        public void Operat_ShouldEnumarateFiles()
        {
            var operat = new Operat();
            var pliki = new[] {
                new PlikOperatu { Plik = "00_A.jpg" },
                new PlikOperatu { Plik = "0_B.jpg" },
                new PlikOperatu { Plik = "01_P.jpg" },
                new PlikOperatu { Plik = "11_O.jpg" }
            };
            foreach(var plik in pliki.Reverse()) operat.Dodaj(plik);
            operat.Pliki.Count().ShouldBe(pliki.Length);
            foreach (var plik in operat.Pliki) plik.Numer.ShouldBe(0);
            operat.ZanumerujPliki();
            for (int i = 0; i < pliki.Length; i++) pliki[i].Numer.ShouldBe(i + 1);
        }

    }
}
