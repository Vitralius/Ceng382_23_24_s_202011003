using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using System.Security.Claims;

namespace WebApp.Pages;
    [Authorize]
    public class AddReservationsModel : PageModel
    {
        private readonly ILogger<AddReservationsModel> _logger;
        private readonly ApplicationDbContext AppDb;
        [BindProperty]
        public Reservation Reservation { get; set; } = default!;
        public List<Room> roomList { get; set; } = default!;

        public AddReservationsModel(ILogger<AddReservationsModel> logger, ApplicationDbContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }
        public void OnGet()
        {
            roomList = (from item in AppDb.Rooms
                    select item).ToList();
            roomList.Sort((room1, room2) => room1.RoomName.CompareTo(room2.RoomName));
        }

         public IActionResult OnPost()
         {
             if (Reservation == null)
             {
                 return Page();
             }
             else if (AppDb.Rooms.FirstOrDefault(item => item.RoomId == Reservation.RoomId).Capacity > 0)
             {
             Reservation.ReserverId = HttpContext.Session.GetString("userId") ?? string.Empty;
             AppDb.Rooms.FirstOrDefault(item => item.RoomId == Reservation.RoomId).Capacity--;
             AppDb.Reservations.Add(Reservation);
             AppDb.SaveChanges();
             _logger.LogInformation("Reservation is added.");
             return RedirectToAction("Get");
             }
             else
             {
                return Page();
             }
         }
    }

