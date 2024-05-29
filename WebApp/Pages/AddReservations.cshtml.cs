using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.CodeAnalysis.Elfie.Extensions;
#nullable disable

namespace WebApp.Pages;
    [Authorize]
    public class AddReservationsModel : PageModel
    {
        private readonly ILogger<AddReservationsModel> _logger;
        private readonly RoomsAndReservationsDatabaseContext AppDb;
        [BindProperty]
        public Reservation Reservation { get; set; } = new Reservation();
        [BindProperty]
        public InputModel Input { get; set; }
        public List<Room> RoomList { get; set; } = new List<Room>();
        public string UserId { get; set; } = default!;
        public AddReservationsModel(ILogger<AddReservationsModel> logger, RoomsAndReservationsDatabaseContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }

        public class InputModel
        {
            [Display(Name = "Room Name")]
            public int RoomId { get; set; }
            [Display(Name = "Date")]
            public DateOnly Date { get; set; }
        }
        private async Task LoadAsync()
        {
            RoomList = await AppDb.Rooms.Where(room => room.Capacity > 0).ToListAsync();
            RoomList.Sort((room1, room2) => room1.RoomName.CompareTo(room2.RoomName));
            Input = new InputModel
            {
                RoomId = 0,
                Date = new DateOnly()
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
             if (Reservation == null)
             {
                _logger.LogInformation("Reservation is null.");
                 return Page();
             }
             var room = await AppDb.Rooms.FirstOrDefaultAsync(item => item.RoomId == Input.RoomId);
             if (room == null)
             {
                _logger.LogInformation("Room is null");
                return Page();
             }
             Reservation = new Reservation
             {
                ReserverId = id,
                Room = room,
                RoomId = Input.RoomId,
                Date = Input.Date,
                IsCanceled = false,
                IsConfirmed = false,
                IsDeleted = false
             };
             await AppDb.Reservations.AddAsync(Reservation);
             _logger.LogInformation("Reservation is added.");
            await AppDb.SaveChangesAsync();
            return RedirectToPage();
         }
    }

