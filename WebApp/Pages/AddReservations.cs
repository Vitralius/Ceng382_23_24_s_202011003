using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;

namespace WebApp.Pages;
    [Authorize]
    public class AddReservationsModel : PageModel
    {
        [BindProperty]
        public Reservation Reservation { get; set; } = default!;

        public ApplicationDbContext AppDb = new();
        public List<Reservation> reservationList { get; set; } = default!;
        public List<Room> roomList { get; set; } = default!;
        public void OnGet()
        {
            reservationList = (from item in AppDb.Reservations
                    select item).ToList();
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
             AppDb.Reservations.Add(Reservation);
             AppDb.SaveChanges();
             return RedirectToAction("Get");
         }
    }

