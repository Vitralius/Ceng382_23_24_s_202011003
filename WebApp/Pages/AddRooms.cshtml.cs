using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

    public class AddRoomsModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = default!;

        public AppDbContext AppDb = new();
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

