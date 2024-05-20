using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;

namespace WebApp.Pages;
    [Authorize]
    public class ListReservationsModel : PageModel
    {
        public ApplicationDbContext AppDb = new();
        public List<Reservation> reservationList { get; set; } = default!;
        public void OnGet()
        {
            reservationList = (from item in AppDb.Reservations
                     select item).ToList();
            reservationList.Sort((reservation1, reservation2) => reservation1.ReserverName.CompareTo(reservation2.ReserverName));
        }
        public IActionResult OnPostDelete(int id)
        {
            if (AppDb.Reservations != null)
            {
                var reservation = AppDb.Reservations.Find(id);
                if (reservation != null)
                {
                    AppDb.Reservations.Remove(reservation);
                    AppDb.SaveChanges();
                }
         }

         return RedirectToAction("Get");
     }
    }
