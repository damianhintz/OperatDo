using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class RepozytoriumOperatówTest
    {
        [TestMethod]
        public void RepozytoriumOperatów_ShouldBeEmpty()
        {
            var operaty = new RepozytoriumOperatów();
            operaty.Operaty.ShouldBeEmpty();
            operaty.ShouldBeEmpty();
            operaty.ZnalezioneOperaty.ShouldBeEmpty();
            operaty.NieznalezioneOperaty.ShouldBeEmpty();
        }

        [TestMethod]
        public void RepozytoriumOperatów_ShouldAddOneOperat()
        {
            var operaty = new RepozytoriumOperatów();
            var operat = new Operat() { IdZasobu = "P.2801.2016.1" };
            operaty.Dodaj(operat);
            operaty.Count().ShouldBe(1);
            operaty["P.2801.2016.1"].ShouldBeSameAs(operat);
            operaty.NieznalezioneOperaty.ShouldHaveSingleItem();
        }

        [TestMethod]
        public void RepozytoriumOperatów_ShouldNotAddOperatWithoutId()
        {
            var operaty = new RepozytoriumOperatów();
            Should.Throw<ArgumentNullException>(() =>
            {
                operaty.Dodaj(new Operat());
            });
        }

        [TestMethod]
        public void RepozytoriumOperatów_ShouldNotAddNullOperat()
        {
            var operaty = new RepozytoriumOperatów();
            Should.Throw<ArgumentNullException>(() =>
            {
                operaty.Dodaj(null);
            });
        }

        [TestMethod]
        public void RepozytoriumOperatów_ShouldNotAddTheSameOperat()
        {
            var operaty = new RepozytoriumOperatów();
            var operat = new Operat() { IdZasobu = "P.2801.2016.1" };
            Should.Throw<InvalidOperationException>(() =>
            {
                operaty.Dodaj(operat);
                operaty.Dodaj(operat);
            });
        }

        [TestMethod]
        public void RepozytoriumOperatów_ShouldRemoveOperat()
        {
            var operaty = new RepozytoriumOperatów();
            var operat1 = new Operat() { IdZasobu = "P.2801.2016.1" };
            var operat2 = new Operat() { IdZasobu = "P.2801.2016.2" };
            operaty.Dodaj(operat1);
            operaty.Dodaj(operat2);
            operaty.Count().ShouldBe(2);
            operaty.Usuń(operat2);
            operaty.Count().ShouldBe(1);
            operaty.ShouldContain(operat1);
        }
    }
}
