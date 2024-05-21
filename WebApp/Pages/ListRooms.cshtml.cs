using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Pages;
    [Authorize]
    public class ListRoomsModel : PageModel
    {
        public ApplicationDbContext AppDb = new();
        public List<Room> roomList { get; set; } = default!;
        public string UserId { get; set; } = default!;
        private readonly ILogger<ListRoomsModel> _logger;
        public ListRoomsModel(ILogger<ListRoomsModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            roomList = (from item in AppDb.Rooms
                     select item).ToList();
            roomList.Sort((room1, room2) => room1.RoomName.CompareTo(room2.RoomName));
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
                    _logger.LogInformation("Room is deleted.");
                }
         }

         return RedirectToAction("Get");
     }
    }
