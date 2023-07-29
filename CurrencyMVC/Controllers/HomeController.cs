using Currency;
using CurrencyMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CurrencyMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly CurrencyServices _currencyServices;

        public HomeController(CurrencyServices services)
        {
            _currencyServices = services;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}