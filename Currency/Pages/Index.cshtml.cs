using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Pages
{
    public class IndexModel : PageModel
    {
        public readonly CurrencyServices _service;
        [BindProperty]
        public UserBindingModel Input { get; set; }
        public SelectListItem[] CurrencyCodes { get; set; }
        public string[] CurrencyCodesList { get; set; }
        public IndexModel(CurrencyServices services)
        {
            _service = services;
            CurrencyCodes = _service.GetCurrencies();
        }
        public void OnGet()
        {
            CurrencyCodesList = _service.CurrenciesList;
        }
        public IActionResult OnPost()
        {
            if (Input.CurrencyOut == Input.CurrencyIn)
            {
                ModelState.AddModelError(string.Empty, "Валюта не может быть обменняа на саму себя");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var model = _service.AddDataToModel(Input.CurrencyOut, Input.CurrencyIn, Input.Quantity);
            var result = _service.GetAsync(model);

            if (result.Result == null)
            {
                return RedirectToPage("Success", new
                {
                    from = Input.CurrencyOut,
                    to = Input.CurrencyIn,
                });
            }
            
            return RedirectToPage("Success", new
            {
                quantity = result.Result.result,
                amount = result.Result.query.amount,
                from = result.Result.query.from,
                to = result.Result.query.to
            });
        }
        public class UserBindingModel
        {
            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode]
            [Display(Name = "Отдаёте")]
            public string CurrencyOut { get; set; }

            [Required]
            [StringLength(3, MinimumLength = 3)]
            [CurrencyCode]
            [Display(Name = "Получаете")]
            public string CurrencyIn { get; set; }

            [Required(ErrorMessage = "Укажите значение")]
            [Range(1.00f, 1000000.00f, ErrorMessage = "Значение может быть в диапазоне от 1 до 1000000")]
            [Display(Name = "Значение")]
            public float? Quantity { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Укажите Ваш email адрес")]
            public string Email { get; set; } = "test@example.com";
        }
    }
}