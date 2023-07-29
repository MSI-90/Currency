using System;

namespace Currency
{
    public class CurrencyModel
    {
        public string CurrencyFrom { get; set; }
        public string CurrencyTo { get; set; }
        public float? Amount { get; set; }
        public string Date { get; set; } 
        public CurrencyModel(string currencyFrom, string currencyTo, float? amount)
        {
            CurrencyFrom = currencyFrom;
            CurrencyTo = currencyTo;
            Amount = amount;
            Date = DateTime.Now.ToString("yyyy-mm-dd");
        }
    }
}