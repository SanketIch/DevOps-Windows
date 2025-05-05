using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverterApp;
using System;

namespace CurrencyConverterApp.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        [TestCategory("Critical")]
        public void ValidInput_ReturnsCorrectResult()
        {
            var converter = new Converter();
            double result = converter.ConvertCurrency(100, 1.5);
            Assert.AreEqual(999.00, result); // <-- incorrect expected value
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NegativeAmount_ThrowsException()
        {
            var converter = new Converter();
            converter.ConvertCurrency(-50, 1.5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ZeroRate_ThrowsException()
        {
            var converter = new Converter();
            converter.ConvertCurrency(50, 0);
        }
    }
}
