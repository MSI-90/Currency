using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Currency
{
    public class Settings
    {
        public IConfiguration configuration { get; set; }
        public Settings() 
        {
            var configurationBinder = new ConfigurationBuilder();
            configurationBinder.AddInMemoryCollection(new Dictionary<string, string>
            {
                { "DefaultConnection", @"https://api.apilayer.com" },
                { "apikey", "xBIE0karU1KaO6URHY8e0Us0HsX2Jswf" }
            });
            configuration = configurationBinder.Build();
        }
        
        
    }
}
