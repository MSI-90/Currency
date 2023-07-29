using Currency.Pages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Currency
{
    public class CurrencyCodeAttribute : ValidationAttribute
    {
        private static Type Currencies { get; } = typeof(Currencies);
        public string[] CurrencyList { get; set; }
        public CurrencyCodeAttribute()
        {
            CurrencyList = CurrencyData();
        }
        public string[] CurrencyData() 
        {
            PropertyInfo[] properties = Currencies.GetProperties();
            string[] currenciesList = new string[properties.Length];
            for (int i = 0; i < properties.Length; i++)
            {
                currenciesList[i] = properties[i].Name.Trim();
            }
            return currenciesList;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var code = value as string;
            string defaultOption = "***не выбрано***";
            if (code == null || !CurrencyList.Contains(code))
            {
                return new ValidationResult("Неверный выбор");
            }

            return ValidationResult.Success;
        }
    }
}
