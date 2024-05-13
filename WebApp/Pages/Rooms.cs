using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

    public class RoomsModel : PageModel
    {
        private readonly ILogger<RoomsModel> _logger;

        public RoomsModel(ILogger<RoomsModel> logger)
    {
        _logger = logger;
    }
        public void OnGet()
        {
        }
    }

