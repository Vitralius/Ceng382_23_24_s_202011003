using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Pages;
    [Authorize]
    public class ReservationScheduleModel : PageModel
    {
        private readonly RoomsAndReservationsDatabaseContext AppDb = new();
        private readonly ILogger<ListReservationsModel> _logger;
        public List<Reservation> ReservationList { get; set; } = new List<Reservation>();
        public List<Room> RoomList { get; set; } = new List<Room>();
        public ReservationLog reservationLog { get; set; } = new ReservationLog();
        public string UserId { get; set; } = null!;
        public List<DateOnly> Dates = new List<DateOnly>();
        public ReservationScheduleModel(ILogger<ListReservationsModel> logger, RoomsAndReservationsDatabaseContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }
        private async Task LoadAsync()
        {
            var id = HttpContext.Session.GetString("userId") ?? string.Empty;
            var today = DateTime.Now;
            for(int i=0; i<7; i++)
            {
                Dates.Add(DateOnly.FromDateTime(today).AddDays(i));
            };
            ReservationList = await AppDb.Reservations.Where(reservation => reservation.IsDeleted == false && reservation.ReserverId == id && reservation.Date.DayOfYear >= today.DayOfYear && reservation.Date.DayOfYear < Dates[6].DayOfYear).ToListAsync();
            RoomList = await AppDb.Rooms.ToListAsync();
        }
        public async Task<IActionResult> OnGetAsync()
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            await LoadAsync();
            return Page();
        }

    }


