using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class IdZasobuTest
    {
        [TestMethod]
        public void IdZasobu_ShouldBeInitializedWithDefaults()
        {
            var id = new IdZasobu(1, 2000);
            id.Numer.ShouldBe(1);
            id.Rok.ShouldBe(2000);
            id.Kod.ShouldBe("2801");
            id.Id.ShouldBe("P.2801.2000.1");
            id.ToString().ShouldBe("P.2801.2000.1");
        }

        [TestMethod]
        public void IdZasobu_ShouldBeParsedFromString()
        {
            var id = IdZasobu.Parse("P.2001.2000.1");
            id.Numer.ShouldBe(1);
            id.Rok.ShouldBe(2000);
            id.Kod.ShouldBe("2001");
            id.Id.ShouldBe("P.2001.2000.1");
            id.ToString().ShouldBe("P.2001.2000.1");
        }
    }
}
