using System.Security.Claims;
using WebApp.Data;
using WebApp.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace WebApp.Areas.Identity.Pages.Account.Manage
{
    public class MyReservationsModel : PageModel
    {
        public readonly RoomsAndReservationsDatabaseContext AppDb;
        private readonly UserManager<IdentityUser> UserManager;
        public List<Reservation> ReservationList { get; set; } = new List<Reservation>();
        public MyReservationsModel (UserManager<IdentityUser> _userManager, RoomsAndReservationsDatabaseContext Db)
        {
            UserManager = _userManager;
            AppDb = Db;     
        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userId = await UserManager.GetUserIdAsync(user);
            ReservationList = await AppDb.Reservations.Where(item => item.ReserverId == userId && item.IsDeleted == false).ToListAsync();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }
        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var find = await AppDb.Reservations.FirstOrDefaultAsync(c => c.ReservationId == id);
            if (find != null)
            {
                var room = await AppDb.Rooms.FirstOrDefaultAsync(item => item.RoomId == find.RoomId);
                if (room != null)
                {
                    room.Capacity = room.Capacity + 1;
                    find.IsCanceled = true;
                }
            } 
            await AppDb.SaveChangesAsync();

            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostDelete(int id)
        {
            var user = await UserManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{UserManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }
            var find = await AppDb.Reservations.FirstOrDefaultAsync(c => c.ReservationId == id);
            if (find != null)
            {
                find.IsDeleted = true;
            }   
            
            await AppDb.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
