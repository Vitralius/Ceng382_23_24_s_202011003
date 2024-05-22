using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using WebApp.Data;
using WebApp.Models;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Mono.TextTemplating;

namespace WebApp.Pages;
    [Authorize]
    public class ListReservationsModel : PageModel
    {
        private readonly ApplicationDbContext AppDb = new();
        private readonly ILogger<ListReservationsModel> _logger;
        public List<Reservation> reservationList { get; set; } = default!;
        public List<Room> roomList { get; set; } = default!;
        public string UserId { get; set; } = default!;
        public string RoomNameSort { get; set; } = null!;
        public string RoomCapacitySort { get; set; } = null!;
        public string DateSort { get; set; }= null!;
        public string RoomNameFilter { get; set; } = null!;
        public int RoomCapacityFilter { get; set; }
        public DateOnly DateFilter { get; set; }
        public ListReservationsModel(ILogger<ListReservationsModel> logger, ApplicationDbContext Db)
        {
            AppDb = Db;
            _logger = logger;
        }

        public void OnGet(IdentityUser user, string sortOrder, string searchString1, string searchString2, string searchString3)
        {
            //Listing part
            UserId = HttpContext.Session.GetString("userId") ?? string.Empty;
            var today = DateTime.Today;
            var lastday = today.AddDays(7);
            reservationList = (from item in AppDb.Reservations
                       where item.Date.DayOfYear >= today.DayOfYear && item.Date.DayOfYear  < lastday.DayOfYear 
                       select item).ToList();
            roomList = (from item in AppDb.Rooms
                     select item).ToList();

            RoomNameFilter = searchString1;
            RoomCapacityFilter = Convert.ToInt32(searchString2);
            if(!String.IsNullOrEmpty(searchString3))
            {
                DateFilter = DateOnly.Parse(searchString3);
            }

            //Filtering and Sorting part
            RoomNameSort = String.IsNullOrEmpty(sortOrder) ? "roomname_desc" : "";
            RoomCapacitySort = sortOrder == "Capacity" ? "capacity_desc" : "Capacity";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            IQueryable<Reservation> sortingList = from r in AppDb.Reservations
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
                default:
                    sortingList = sortingList.OrderBy(s => s.Room.RoomName);
                    break;
            }
            reservationList = sortingList.ToList();  
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
