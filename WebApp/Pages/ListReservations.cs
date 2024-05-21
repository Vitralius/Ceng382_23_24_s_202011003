using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApp.Pages;
    [Authorize]
    public class ListReservationsModel : PageModel
    {
        private readonly ApplicationDbContext AppDb = new();
        private readonly ILogger<ListReservationsModel> _logger;
        public List<Reservation> reservationList { get; set; } = default!;
        public List<Room> roomList { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public readonly UserManager<IdentityUser> _IUserManager;
        public ListReservationsModel(ILogger<ListReservationsModel> logger, ApplicationDbContext Db, UserManager<IdentityUser> IUserManager)
        {
            _IUserManager = IUserManager;
            AppDb = Db;
            _logger = logger;
        }

        public void OnGet(IdentityUser user)
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            var today = DateTime.Now;
            var lastday = today.AddDays(7);
            reservationList = (from item in AppDb.Reservations
                       where item.Date.DayOfYear >= today.DayOfYear && item.Date.DayOfYear  < lastday.DayOfYear 
                       select item).ToList();
            reservationList.Sort((reservation1, reservation2) => reservation1.Date.CompareTo(reservation2.Date));
            roomList = (from item in AppDb.Rooms
                     select item).ToList();
        }
        public IActionResult OnPostDelete(int id)
        {
            if (AppDb.Reservations != null)
            {
                var reservation = AppDb.Reservations.Find(id);
                if (reservation != null)
                {
                    AppDb.Reservations.Remove(reservation);
                    AppDb.Rooms.FirstOrDefault(item => item.RoomId == reservation.RoomId).Capacity++;
                    AppDb.SaveChanges();
                    
                    _logger.LogInformation("Reservation is deleted.");
                }
         }

         return RedirectToAction("Get");
     }
    }
