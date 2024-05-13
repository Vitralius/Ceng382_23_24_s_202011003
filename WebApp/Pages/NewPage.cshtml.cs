using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

    public class NewPageModel : PageModel
    {
        private readonly ILogger<NewPageModel> _logger;

        public NewPageModel(ILogger<NewPageModel> logger)
    {
        _logger = logger;
    }
        public void OnGet()
        {
        }
    }

