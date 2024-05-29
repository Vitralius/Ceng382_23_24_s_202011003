using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace WebApp.Pages;
    [Authorize]
    public class ListRoomsModel : PageModel
    {
        private readonly RoomsAndReservationsDatabaseContext AppDb;
        public List<Room> RoomList { get; set; } = new List<Room>();
        public string UserId { get; set; } = default!;
        private readonly ILogger<ListRoomsModel> _logger;
        public RoomLog roomLog { get; set; } = new RoomLog();
        public ListRoomsModel(ILogger<ListRoomsModel> logger, RoomsAndReservationsDatabaseContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }
        private async Task LoadAsync(string searchString)
        {
            RoomList = await AppDb.Rooms.Where(r => r.IsDeleted==false).ToListAsync();
            RoomList.Sort((room1, room2) => room1.RoomName.CompareTo(room2.RoomName));
            IQueryable<Room> sortingList = from r in RoomList.AsQueryable()
                                        select r;
            if (!String.IsNullOrEmpty(searchString) && searchString == "false")
            {
                sortingList = sortingList.Where(r => r.Capacity > 0);
            }
            else if (!String.IsNullOrEmpty(searchString) && searchString == "true")
            {
                sortingList = sortingList.Where(r => r.Capacity == 0);
            }
            RoomList = sortingList.ToList();
        }
        public async Task<IActionResult> OnGetAsync(string searchString)
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            await LoadAsync(searchString);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (AppDb.Rooms == null)
            {
                return Page();
            }
            var room = await AppDb.Rooms.FirstOrDefaultAsync(room => room.RoomId == id);
            if (room == null)
            {
                return Page();
            }
             room.IsDeleted = true;
             _logger.LogInformation("Room is deleted.");
            await AppDb.SaveChangesAsync();
            await LogAsync(room.RoomId, "DELETED");
             return RedirectToPage();
        }
        public async Task<IActionResult> OnPostCloseAsync(int id)
        {
            if (AppDb.Rooms == null)
            {
                return Page();
            }
            var room = await AppDb.Rooms.FirstOrDefaultAsync(room => room.RoomId == id);
            if (room == null)
            {
                return Page();
            }
             room.IsReservable = false;
             _logger.LogInformation("Room is closed.");
            await AppDb.SaveChangesAsync();
            await LogAsync(room.RoomId, "CLOSED");
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostOpenAsync(int id)
        {
            if (AppDb.Rooms == null)
            {
                return Page();
            }
            var room = await AppDb.Rooms.FirstOrDefaultAsync(room => room.RoomId == id);
            if (room == null)
            {
                return Page();
            }
             room.IsReservable = true;
             _logger.LogInformation("Room is opened.");
            await AppDb.SaveChangesAsync();
            await LogAsync(room.RoomId, "OPENED");
            return RedirectToPage();
        }
        private async Task LogAsync(int id, string S)
        {
            switch (S)
            {
                case "DELETED":
                roomLog = new RoomLog 
                {
                    Status = S,
                    IsDeleted = true,
                    UserId = HttpContext.Session.GetString("userId") ?? string.Empty,
                    LogDate = DateTime.Now,
                    RoomId = id, 
                };
                break;

                case "CLOSED":
                roomLog = new RoomLog 
                {
                    Status = S,
                    IsDeleted = false,
                    UserId = HttpContext.Session.GetString("userId") ?? string.Empty,
                    LogDate = DateTime.Now,
                    RoomId = id, 
                };
                break;

                case "OPENED":
                roomLog = new RoomLog 
                {
                    Status = S,
                    IsDeleted = false,
                    UserId = HttpContext.Session.GetString("userId") ?? string.Empty,
                    LogDate = DateTime.Now,
                    RoomId = id, 
                };
                break;
            }
            await AppDb.RoomLogs.AddAsync(roomLog);
            await AppDb.SaveChangesAsync();
        }
    }
