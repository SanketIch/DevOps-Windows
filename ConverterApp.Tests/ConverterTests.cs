using Microsoft.VisualStudio.TestTools.UnitTesting;
using CurrencyConverterApp;
using System;

namespace CurrencyConverterApp.Tests
{
    [TestClass]
    public class ConverterTests
    {
        [TestMethod]
        public void ConvertCurrency_ValidInput_ReturnsCorrectResult()
        {
            var converter = new Converter();
            double result = converter.ConvertCurrency(100, 1.5);
            Assert.AreEqual(150.00, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertCurrency_NegativeAmount_ThrowsException()
        {
            var converter = new Converter();
            converter.ConvertCurrency(-50, 1.5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ConvertCurrency_ZeroRate_ThrowsException()
        {
            var converter = new Converter();
            converter.ConvertCurrency(50, 0);
        }
    }
}
