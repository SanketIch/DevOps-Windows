using System;

namespace CurrencyConverterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Currency Converter");

            try
            {
                Console.Write("Enter amount in USD: ");
                double amount = Convert.ToDouble(Console.ReadLine());

                Console.Write("Enter conversion rate: ");
                double rate = Convert.ToDouble(Console.ReadLine());

                Converter converter = new Converter();
                double result = converter.ConvertCurrency(amount, rate);

                Console.WriteLine($"Converted amount: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
