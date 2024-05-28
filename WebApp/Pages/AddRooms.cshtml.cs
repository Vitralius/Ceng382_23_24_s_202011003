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
        public List<Room> RoomList { get; set; } = new List<Room>();
        private readonly ILogger<AddRoomsModel> _logger;
        public string UserId { get; set; } = default!;
        public AddRoomsModel(ILogger<AddRoomsModel> logger, RoomsAndReservationsDatabaseContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }
        public class InputModel
        {
            [Display(Name = "Room Name")]
            public string RoomName { get; set; } = default!;
            [Display(Name = "Capacity")]
            public int Capacity { get; set; }
        }
        private async Task LoadAsync()
        {
            RoomList = await AppDb.Rooms.ToListAsync();
            Input = new InputModel
            {
                RoomName = "null",
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
             return RedirectToPage();
        }
    }

