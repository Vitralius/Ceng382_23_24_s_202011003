using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Room
{
    public int RoomId { get; set; }

    public string RoomName { get; set; } = null!;

    public int Capacity { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsReservable { get; set; }

    public string OwnerId { get; set; } = null!;

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
