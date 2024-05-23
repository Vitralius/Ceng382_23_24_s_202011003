using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity;
namespace WebApp.Pages;


public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }    

    public void OnGet()
    {

    }

}
