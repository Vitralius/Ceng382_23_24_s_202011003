using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;

namespace WebApp.Pages;
    [Authorize]
    public class ListRoomsModel : PageModel
    {
        public ApplicationDbContext AppDb = new();
        public List<Room> roomList { get; set; } = default!;
        public void OnGet()
        {
            roomList = (from item in AppDb.Rooms
                     select item).ToList();
        }
        public IActionResult OnPostDelete(int id)
        {
            if (AppDb.Rooms != null)
            {
                var room = AppDb.Rooms.Find(id);
                if (room != null)
                {
                    AppDb.Rooms.Remove(room);
                    AppDb.SaveChanges();
                }
         }

         return RedirectToAction("Get");
     }
    }
