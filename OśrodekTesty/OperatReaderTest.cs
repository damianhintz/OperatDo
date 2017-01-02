using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekPliki;
using OśrodekPliki.Rozszerzenia;
using OśrodekDomena;
using Shouldly;

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
            reader.Pliki.ShouldBeNull();
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
        public void OperatyReader_ShouldReadOperatWith3Files()
        {
            var operaty = new RepozytoriumOperatów();
            var reader = new OperatReader(operaty);
            reader.Wczytaj(folder: @"Samples\Pliki\P.2801.2016.122");
            reader.Operaty.Count().ShouldBe(1);
            reader.PlikiOperatów().Count().ShouldBe(3);
        }

        [TestMethod]
        public void OperatyReader_ShouldDetectEmptyFiles()
        {
            var operaty = new RepozytoriumOperatów();
            var reader = new OperatReader(operaty);
            reader.Wczytaj(folder: @"Samples\Pliki\PustyPlik");
            reader.Operaty.Count().ShouldBe(1);
            reader.PlikiOperatów().Count().ShouldBe(2);
            var pustePliki = reader.PustePliki();
            pustePliki.Count().ShouldBe(1);
            pustePliki.Single().Name.ShouldBe("0.jpg");
        }

        [TestMethod]
        public void OperatyReader_ShouldDetectOtherFiles()
        {
            var operaty = new RepozytoriumOperatów();
            var reader = new OperatReader(operaty);
            reader.Wczytaj(folder: @"Samples\Pliki\DodatkowyPlik");
            reader.Operaty.Count().ShouldBe(1);
            reader.PlikiOperatów().Count().ShouldBe(1);
            reader.PustePliki().Count().ShouldBe(0);
            var dodatkowePliki = reader.DodatkowePliki();
            dodatkowePliki.Count().ShouldBe(1);
            dodatkowePliki.Single().Name.ShouldBe("200.tif");
        }

        [TestMethod]
        public void OperatyReader_ShouldDetectEmptyOperat()
        {
            var operaty = new RepozytoriumOperatów();
            var reader = new OperatReader(operaty);
            reader.Wczytaj(folder: @"Samples\Pliki\PustyOperat");
            reader.Operaty.Count().ShouldBe(0);
            reader.PlikiOperatów().Count().ShouldBe(0);
            reader.PustePliki().Count().ShouldBe(0);
            var dodatkowePliki = reader.DodatkowePliki();
            dodatkowePliki.Count().ShouldBe(1);
            dodatkowePliki.Single().Name.ShouldBe("PustyPlik.txt");
            var pusteOperaty = reader.PusteOperaty();
            pusteOperaty.Count().ShouldBe(1);
            pusteOperaty.Single().ShouldEndWith(@"\PustyOperat");
        }
    }
}
