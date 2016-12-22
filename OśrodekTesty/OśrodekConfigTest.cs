using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekFirebird;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class OśrodekConfigTest
    {
        [TestMethod]
        public void OśrodekConfig_ShouldBeLoadedFromFile()
        {
            var config = OśrodekConfig.Wczytaj(fileName: @"..\..\Samples\Osrodek.json");
            config.ShouldNotBeNull();
            config.Host.ShouldBe("serwer-actina");
            config.Path.ShouldBe("e:/Roboty/2904_modernizacja_Galiny/Dokumentacja/08_dane_zrodlowe/osrodek.test/OSRODEK.FDB");
            config.User.ShouldBe("SYSDBA");
            config.Password.ShouldBe("opgk");
            config.Charset.ShouldBe("WIN1250");
        }
    }
}
