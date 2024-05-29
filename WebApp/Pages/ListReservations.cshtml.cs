using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.DotNet.Scaffolding.Shared.ProjectModel;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace WebApp.Pages;
    [Authorize]
    public class ListReservationsModel : PageModel
    {
        private readonly RoomsAndReservationsDatabaseContext AppDb = new();
        private readonly ILogger<ListReservationsModel> _logger;
        public List<Reservation> ReservationList { get; set; } = new List<Reservation>();
        public List<Room> RoomList { get; set; } = new List<Room>();
        public Reservation AddedReservation { get; set; } = new Reservation();
        [BindProperty]
        public InputModel Input { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string RoomNameSort { get; set; } = null!;
        public string RoomCapacitySort { get; set; } = null!;
        public string DateSort { get; set; }= null!;
        public string IsConfirmedSort { get; set; } = null!;
        public string RoomNameFilter { get; set; } = null!;
        public int RoomCapacityFilter { get; set; }
        public bool IsConfirmedFilter { get; set; }
        public DateOnly DateFilter { get; set; }
        public ListReservationsModel(ILogger<ListReservationsModel> logger, RoomsAndReservationsDatabaseContext Db)
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
        private async Task LoadAsync(string sortOrder, string searchString1, string searchString2, string searchString3, bool searchString4)
        {
            Input = new InputModel
            {
                RoomId = 0,
                Date = new DateOnly()
            };
            //Listing part
            var today = DateTime.Today;
            var lastday = today.AddDays(7-(int)today.DayOfWeek);
            ReservationList = await AppDb.Reservations.Where(reservation => reservation.Date.DayOfYear >= today.DayOfYear && reservation.Date.DayOfYear < lastday.DayOfYear).ToListAsync();
            RoomList = await AppDb.Rooms.ToListAsync();
            // ReservationList.Sort((room1, room2) => room1.RoomName.CompareTo(room2.RoomName));
            RoomNameFilter = searchString1;
            RoomCapacityFilter = Convert.ToInt32(searchString2);
            if(!String.IsNullOrEmpty(searchString3))
            {
                DateFilter = DateOnly.Parse(searchString3);
            }
            IsConfirmedFilter = searchString4;

            //Filtering and Sorting part
            RoomNameSort = String.IsNullOrEmpty(sortOrder) ? "roomname_desc" : "";
            RoomCapacitySort = sortOrder == "Capacity" ? "capacity_desc" : "Capacity";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IsConfirmedSort = String.IsNullOrEmpty(sortOrder) ? "isconfirmed_desc" : "IsConfirmed";
            IQueryable<Reservation> sortingList = from r in ReservationList.AsQueryable()
                                        select r;
            if (!String.IsNullOrEmpty(searchString1))
            {
                sortingList = sortingList.Where(r => r.Room.RoomName.Contains(searchString1));
            }
            else if (!String.IsNullOrEmpty(searchString2))
            {
                sortingList = sortingList.Where(r => r.Room.Capacity == Convert.ToInt32(searchString2));
            }
            else if (!String.IsNullOrEmpty(searchString3))
            {
                sortingList = sortingList.Where(r => r.Date == DateOnly.Parse(searchString3));
            }
            // else
            // {
            //     sortingList = sortingList.Where(r => r.IsConfirmed == searchString4);
            // }

            switch (sortOrder)
            {
                case "roomname_desc":
                    sortingList = sortingList.OrderByDescending(r => r.Room.RoomName);
                    break;
                case "Date":
                    sortingList = sortingList.OrderBy(r => r.Date);
                   break;
                case "date_desc":
                    sortingList = sortingList.OrderByDescending(r => r.Date);
                    break;
                case "Capacity":
                    sortingList = sortingList.OrderBy(r => r.Room.Capacity);
                    break;
                case "capacity_desc":
                    sortingList = sortingList.OrderByDescending(r => r.Room.Capacity);
                    break;
                case "IsConfirmed":
                    sortingList = sortingList.OrderBy(c => c.IsConfirmed);
                    break;
                case "isconfirmed_desc":
                    sortingList = sortingList.OrderByDescending(c => c.IsConfirmed);
                    break;
                default:
                    sortingList = sortingList.OrderBy(s => s.Room.RoomName);
                    break;
            }
            ReservationList = sortingList.ToList();
        }
        public async Task<IActionResult> OnGetAsync(string sortOrder, string searchString1, string searchString2, string searchString3, bool searchString4)
        {
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            await LoadAsync(sortOrder,searchString1, searchString2, searchString3, searchString4);
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (AppDb.Reservations != null)
            {
                var reservation = await AppDb.Reservations.FirstOrDefaultAsync(reservation => reservation.ReservationId == id);
                if (reservation != null)
                {
                    reservation.IsDeleted = true;
                    _logger.LogInformation("Reservation is deleted");
                }
         }
         await AppDb.SaveChangesAsync();
         return RedirectToPage();
        }
        public async Task<IActionResult> OnPostConfirmAsync(int id)
        {
            if (AppDb.Reservations != null)
            {
                var reservation = await AppDb.Reservations.FirstOrDefaultAsync(reservation => reservation.ReservationId == id);
                if (reservation != null)
                {
                    var room = await AppDb.Rooms.FirstOrDefaultAsync(item => item.RoomId == reservation.RoomId);
                    if (room != null && room.Capacity > 0)
                    {
                        room.Capacity = room.Capacity - 1;
                        reservation.IsConfirmed = true;
                        _logger.LogInformation("Reservation is cofirmed and corresponding room capacity decreased.");
                    }
                    else 
                    {
                        _logger.LogInformation("Room capacity is insufficient.");
                    }
                }
         }
         await AppDb.SaveChangesAsync();
         return RedirectToPage();
     }
        public async Task<IActionResult> OnPostEditAsync()
        {
            var reservation = await AppDb.Reservations.FirstOrDefaultAsync(reservation => reservation.ReservationId == Id);
            if (reservation == null)
            {
              return NotFound();
            }
            var room = await AppDb.Rooms.FirstOrDefaultAsync(item => item.RoomId == Input.RoomId);
            if (room == null)
            {
               _logger.LogInformation("Room is null");
               return Page();
            }
            reservation.RoomId = Input.RoomId;
            reservation.Date = Input.Date;
            AppDb.Update(reservation);
            await AppDb.SaveChangesAsync();
            return RedirectToPage();
        }
    }


