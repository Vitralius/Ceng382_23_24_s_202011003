using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;

namespace WebApp.Pages;
    [Authorize]
    public class AddRoomsModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = default!;

        public ApplicationDbContext AppDb = new();
        public List<Room> roomList { get; set; } = default!;
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
             AppDb.Rooms.Add(Room);
             AppDb.SaveChanges();
             return RedirectToAction("Get");
         }
    }

