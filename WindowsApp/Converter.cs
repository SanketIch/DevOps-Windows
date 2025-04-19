using System;

namespace CurrencyConverterApp
{
    public class Converter
    {
        public double ConvertCurrency(double amount, double rate)
        {
            if (amount < 0 || rate <= 0)
                throw new ArgumentException("Invalid amount or rate");

            return Math.Round(amount * rate, 2);
        }
    }
}
