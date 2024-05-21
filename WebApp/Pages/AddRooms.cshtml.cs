using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages;
    [Authorize]
    public class AddRoomsModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = default!;

        public ApplicationDbContext AppDb = new();
        public List<Room> roomList { get; set; } = default!;
        private readonly ILogger<AddRoomsModel> _logger;
        public AddRoomsModel(ILogger<AddRoomsModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            roomList = (from item in AppDb.Rooms
                     select item).ToList();
        }

         public IActionResult OnPost()
         {
             if (Room == null)
             {
                 return Page();
             }
             Room.OwnerId = HttpContext.Session.GetString("userId") ?? string.Empty;
             AppDb.Rooms.Add(Room);
             AppDb.SaveChanges();
             _logger.LogInformation("Room is added.");
             return RedirectToAction("Get");
         }
    }

