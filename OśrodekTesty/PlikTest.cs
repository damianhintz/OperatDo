using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using OśrodekDomena.Rozszerzenia;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class PlikTest
    {
        [TestMethod]
        public void Dokument_ShouldBeEmpty()
        {
            var dokument = new PlikOperatu();
            dokument.Id.ShouldBeNull();
            dokument.Operat.ShouldBeNull();
            dokument.PlikId.ShouldBeNull();
            dokument.Plik.ShouldBeNull();
            dokument.Typ.ShouldBe(TypPliku.Jpg);
            dokument.Numer.ShouldBe(0);
            dokument.Rozmiar.ShouldBeNull();
            dokument.Rozdzielczość.ShouldBeNull();
        }

        [TestMethod]
        public void Dokument_ShouldHasDpi200x200()
        {
            var dokument = new PlikOperatu
            {
                Plik = @"Samples\200.jpg"
            };
            var plik = dokument.WczytajPlik();
            plik.ShouldNotBeNull();
            var dpi = plik.OdczytajRozdzielczość();
            dpi.Dpi.ShouldBe(200);
        }

        [TestMethod]
        public void Dokument_ShouldHasDpi300x300()
        {
            var dokument = new PlikOperatu
            {
                Plik = @"Samples\300.jpg"
            };
            var plik = dokument.WczytajPlik();
            plik.ShouldNotBeNull();
            var dpi = plik.OdczytajRozdzielczość();
            dpi.Dpi.ShouldBe(300);
        }

        [TestMethod]
        public void Dokument_ShouldHasDpi400x400()
        {
            var dokument = new PlikOperatu
            {
                Plik = @"Samples\400.jpg"
            };
            var plik = dokument.WczytajPlik();
            plik.ShouldNotBeNull();
            var dpi = plik.OdczytajRozdzielczość();
            dpi.Dpi.ShouldBe(400);
        }

        [TestMethod]
        public void Dokument_ShouldHasDpi600x600()
        {
            var dokument = new PlikOperatu
            {
                Plik = @"Samples\600.jpg"
            };
            var plik = dokument.WczytajPlik();
            plik.ShouldNotBeNull();
            var dpi = plik.OdczytajRozdzielczość();
            dpi.Dpi.ShouldBe(600);
        }
    }
}
