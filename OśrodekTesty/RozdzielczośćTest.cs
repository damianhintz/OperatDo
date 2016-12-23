using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OśrodekDomena;
using Shouldly;

namespace OśrodekTesty
{
    [TestClass]
    public class RozdzielczośćTest
    {
        [TestMethod]
        public void Rozdzielczość_ShouldBeInitialized()
        {
            var dpi = new Rozdzielczość(300);
            dpi.Dpi.ShouldBe(300);
            dpi.Ppm.ShouldBe(11811);
        }
    }
}
