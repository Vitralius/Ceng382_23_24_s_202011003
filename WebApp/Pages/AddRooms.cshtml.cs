using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
#nullable disable

namespace WebApp.Pages;
    [Authorize]
    public class AddRoomsModel : PageModel
    {
        [BindProperty]
        public Room Room { get; set; } = new Room();
        [BindProperty]
        public InputModel Input { get; set; }
        private readonly RoomsAndReservationsDatabaseContext AppDb;
        private readonly ILogger<AddRoomsModel> _logger;
        public List<Room> RoomList { get; set; } = new List<Room>();
        public RoomLog roomLog { get; set; } = new RoomLog();
        public string UserId { get; set; } = default!;
        public string PlaceHolder { get; set; }
        public AddRoomsModel(ILogger<AddRoomsModel> logger, RoomsAndReservationsDatabaseContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }
        public class InputModel
        {
            [Display(Name = "Room Name")]
            public string RoomName { get; set; }
            [Display(Name = "Capacity")]
            public int Capacity { get; set; }
        }
        private async Task LoadAsync()
        {
            RoomList = await AppDb.Rooms.ToListAsync();
            PlaceHolder = null;
            Input = new InputModel
            {
                RoomName =  null,
                Capacity = 0
            };
        }
        public async Task<IActionResult> OnGetAsync()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            await LoadAsync();
            return Page();
        }

         public async Task<IActionResult> OnPostAddAsync()
         {
             var id =  HttpContext.Session.GetString("userId") ?? string.Empty;
             if (Room == null)
             {
                _logger.LogInformation("Room is null.");
                 return Page();
             }
             if (Input.RoomName == null)
             {
                PlaceHolder="Must enter a valid room name";
                return Page();
             }
             Room = new Room
             {
                RoomName = Input.RoomName,
                Capacity = Input.Capacity,
                IsDeleted = false,
                IsReservable = true,
                OwnerId = id
             }; 
             await AppDb.Rooms.AddAsync(Room);
             await AppDb.SaveChangesAsync();
             _logger.LogInformation("Room is added.");

            var room = await AppDb.Rooms.FirstOrDefaultAsync(item => item.RoomId == Room.RoomId);
            if (room == null)
            {
                return Page();
            }
            await LogAsync(room.RoomId);
            return RedirectToPage();
        }
        private async Task LogAsync(int id)
        {
             roomLog = new RoomLog 
            {
                Status = "CREATED",
                IsDeleted = false,
                UserId = HttpContext.Session.GetString("userId") ?? string.Empty,
                LogDate = DateTime.Now,
                RoomId = id, 
            };
            await AppDb.RoomLogs.AddAsync(roomLog);
            await AppDb.SaveChangesAsync();
        }
    }

