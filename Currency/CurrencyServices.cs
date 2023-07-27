using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Currency
{
    public class CurrencyServices
    {
        //private static readonly IConfiguration _config;
        private static Type CurrencyListType { get; } = typeof(Currencies);
        public string[] CurrenciesList { get; set; }
        public CurrencyModel Model { get; set; }
        public SelectListItem[] GetCurrencies()
        {
            PropertyInfo[] propertiesName = CurrencyListType.GetProperties();
            SelectListItem[] propertyNameList = new SelectListItem[propertiesName.Length];
            CurrenciesList = new string[propertiesName.Length];

            if (propertiesName != null)
            {
                for (int i = 0; i < propertiesName.Length; i++)
                {
                    propertyNameList[i] = new SelectListItem
                    {
                        Text = propertiesName[i].Name.Trim(),
                        Value = propertiesName[i].Name.Trim()
                    };
                    CurrenciesList[i] = propertyNameList[i].Value.Trim();
                }
            }
            else
            { propertyNameList = null; }
            
            return propertyNameList;
        }
        public CurrencyModel AddDataToModel(string from, string to, float? amount)
        {
            Model = new CurrencyModel(from, to, amount);

            if (Model == null)
            {
                return null;
            }
            return Model;
        }
        public async Task<Rootobject> GetAsync(CurrencyModel model)
        {
            using (HttpClient client = new HttpClient())
            {
                string connectionString = "https://api.apilayer.com";
                string apikey = "hX33IYWgpZmqKOvdLX3z1d2aw61JVYC4"; //    xBIE0karU1KaO6URHY8e0Us0HsX2Jswf
                using (HttpRequestMessage request = new HttpRequestMessage())
                {
                    string digit = model.Amount.ToString();
                    request.RequestUri = new Uri(connectionString + $"/currency_data/convert?to={model.CurrencyTo}&from={model.CurrencyFrom}&amount={(model.Amount).ToString().Replace(',', '.')}");
                    request.Method = HttpMethod.Get;
                    request.Headers.Add(nameof(apikey), apikey);

                    HttpResponseMessage response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var obj = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Rootobject>(obj);

                        return result;
                    }
                    return null;
                }
            }
        }
    }
}
