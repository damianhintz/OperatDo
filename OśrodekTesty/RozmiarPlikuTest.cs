using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class RozmiarPlikuTest
    {
        [TestMethod]
        public void RozmiarPliku_ShouldBe0()
        {
            var rozmiar = new RozmiarPliku(bajty: 0);
            rozmiar.Bajty.ShouldBe(0);
            rozmiar.KiloBajty.ShouldBe(0);
            rozmiar.MegaBajty.ShouldBe(0);
            rozmiar.GigaBajty.ShouldBe(0);
        }

        [TestMethod]
        public void RozmiarPliku_ShouldBe1024()
        {
            var rozmiar = new RozmiarPliku(bajty: 1024);
            rozmiar.Bajty.ShouldBe(1024);
            rozmiar.KiloBajty.ShouldBe(1);
            rozmiar.MegaBajty.ShouldBe(0);
            rozmiar.GigaBajty.ShouldBe(0);
        }

        [TestMethod]
        public void RozmiarPliku_ShouldBe1MB()
        {
            var rozmiar = new RozmiarPliku(bajty: 1024 * 1024);
            rozmiar.Bajty.ShouldBe(1024 * 1024);
            rozmiar.KiloBajty.ShouldBe(1024);
            rozmiar.MegaBajty.ShouldBe(1);
            rozmiar.GigaBajty.ShouldBe(0);
        }

        [TestMethod]
        public void RozmiarPliku_ShouldBe1GB()
        {
            var rozmiar = new RozmiarPliku(bajty: 1024 * 1024 * 1024);
            rozmiar.Bajty.ShouldBe(1024 * 1024 * 1024);
            rozmiar.KiloBajty.ShouldBe(1024 * 1024);
            rozmiar.MegaBajty.ShouldBe(1024);
            rozmiar.GigaBajty.ShouldBe(1);
        }

        [TestMethod]
        public void RozmiarPliku_ShouldBe200FileLength()
        {
            var rozmiar = RozmiarPliku.Wczytaj(@"Samples\200.jpg");
            rozmiar.Bajty.ShouldBe(610765);
        }
    }
}
