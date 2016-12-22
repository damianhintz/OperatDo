using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using OśrodekPliki;
using OśrodekTesty.Rozszerzenia;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class OperatWriterTest
    {
        [TestMethod]
        public void OperatWriter_ShouldBeEmpty()
        {
            var writer = new OperatWriter();
            writer.OperatDb.ShouldBeNull();
            writer.PlikDb.ShouldBeNull();
        }

        [TestMethod]
        public void OperatWriter_ShouldNotFindOperat()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var writer = new OperatWriter { OperatDb = operatDb };
            writer.WczytajOperat(operat).ShouldBeFalse();
        }

        [TestMethod]
        public void OperatWriter_ShouldNotFindNullOperat()
        {
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var writer = new OperatWriter { OperatDb = operatDb };
            Should.Throw<ArgumentNullException>(() =>
            {
                writer.WczytajOperat(null);
            });
        }

        [TestMethod]
        public void OperatWriter_ShouldFindOperat()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var operatId = operatDb.DodajOperat('E', 'P', "2801", 2016, 1);
            var writer = new OperatWriter { OperatDb = operatDb };
            writer.WczytajOperat(operat).ShouldBeTrue();
            operat.Id.ShouldBe(operatId);
            operat.Typ.ShouldBe('E');
        }

        [TestMethod]
        public void OperatWriter_ShouldNotAddNullOperat()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var writer = new OperatWriter { OperatDb = operatDb };
            Should.Throw<ArgumentNullException>(() =>
            {
                writer.ZapiszOperat(null);
            });
        }

        [TestMethod]
        public void OperatWriter_ShouldNotAddOperatWithNoId()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var writer = new OperatWriter { OperatDb = operatDb };
            Should.Throw<InvalidOperationException>(() =>
            {
                writer.ZapiszOperat(operat);
            });
        }

        [TestMethod]
        public void OperatWriter_ShouldNotAddOperatWithDocumentsInDatabase()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var dokument = new DokumentOperatu { Plik = "*.jpg", Rozdzielczość = 300 };
            operat.Dodaj(dokument);
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var operatTyp = 'E';
            var operatId = operatDb.DodajOperat("P.2801.2016.1", operatTyp);
            var plikiDb = OśrodekDbRozszerzenia.SamplePlikiDb();
            var plikId = plikiDb.DodajPlik(null);
            operatDb.DodajDokument(operatId, operatTyp, dokument.Plik, dokument.Rozdzielczość, plikId);
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(1);
            plikiDb.PoliczPliki().ShouldBe(1);
            var writer = new OperatWriter
            {
                OperatDb = operatDb,
                PlikDb = plikiDb
            };
            writer.WczytajOperat(operat).ShouldBeTrue(); //Odczytaj operat
            operat.Id.ShouldBe(operatId);
            operat.Typ.ShouldBe(operatTyp);
            writer.ZapiszOperat(operat).ShouldBeFalse(); //Zapisz operat
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(1);
            plikiDb.PoliczPliki().ShouldBe(1);
        }

        [TestMethod]
        public void OperatWriter_ShouldAddOperatWithNoDocumentsAndNoFiles()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var operatTyp = 'E';
            var operatId = operatDb.DodajOperat("P.2801.2016.1", operatTyp);
            var plikiDb = OśrodekDbRozszerzenia.SamplePlikiDb();
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(0);
            plikiDb.PoliczPliki().ShouldBe(0);
            var writer = new OperatWriter
            {
                OperatDb = operatDb,
                PlikDb = plikiDb
            };
            writer.WczytajOperat(operat).ShouldBeTrue(); //Odczytaj operat
            operat.Id.ShouldBe(operatId);
            operat.Typ.ShouldBe(operatTyp);
            writer.ZapiszOperat(operat).ShouldBeTrue(); //Zapisz operat
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(0);
            plikiDb.PoliczPliki().ShouldBe(0);
        }

        [TestMethod]
        public void OperatWriter_ShouldAddOperatWithNoDocuments()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var dokument = new DokumentOperatu
            {
                Plik = @"..\..\Samples\300.jpg",
                Rozdzielczość = 300
            };
            operat.Dodaj(dokument);
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var operatTyp = 'E';
            var operatId = operatDb.DodajOperat("P.2801.2016.1", operatTyp);
            var plikiDb = OśrodekDbRozszerzenia.SamplePlikiDb();
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(0);
            plikiDb.PoliczPliki().ShouldBe(0);
            var writer = new OperatWriter
            {
                OperatDb = operatDb,
                PlikDb = plikiDb
            };
            writer.WczytajOperat(operat).ShouldBeTrue(); //Odczytaj operat
            operat.Id.ShouldBe(operatId);
            operat.Typ.ShouldBe(operatTyp);
            writer.ZapiszOperat(operat).ShouldBeTrue(); //Zapisz operat
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(1);
            plikiDb.PoliczPliki().ShouldBe(1);
        }

        [TestMethod]
        public void OperatWriter_ShouldNotAddOperatIfNoFile()
        {
            var operat = new Operat { IdZasobu = "P.2801.2016.1" };
            var dokument = new DokumentOperatu
            {
                Plik = @"..\..\Samples\400.jpg",
                Rozdzielczość = 300
            };
            operat.Dodaj(dokument);
            var operatDb = OśrodekDbRozszerzenia.SampleDb();
            var operatTyp = 'E';
            var operatId = operatDb.DodajOperat("P.2801.2016.1", operatTyp);
            var plikiDb = OśrodekDbRozszerzenia.SamplePlikiDb();
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(0);
            plikiDb.PoliczPliki().ShouldBe(0);
            var writer = new OperatWriter
            {
                OperatDb = operatDb,
                PlikDb = plikiDb
            };
            writer.WczytajOperat(operat).ShouldBeTrue(); //Odczytaj operat
            operat.Id.ShouldBe(operatId);
            operat.Typ.ShouldBe(operatTyp);
            writer.ZapiszOperat(operat).ShouldBeFalse(); //Zapisz operat
            operatDb.PoliczOperaty().ShouldBe(1);
            operatDb.PoliczDokumenty().ShouldBe(0);
            plikiDb.PoliczPliki().ShouldBe(0);
        }
    }
}
