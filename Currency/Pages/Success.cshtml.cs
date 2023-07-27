using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Currency.Pages
{
    public class SuccessModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public InputModel Input { get; set; }
        public void OnGet()
        {
        }
        public class InputModel
        {
            public float Quantity { get; set; }
            public float Amount { get; set; }
            public string From { get; set; }
            public string To { get; set; }
        }
    }
}
