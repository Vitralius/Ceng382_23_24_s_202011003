using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public DateOnly Date { get; set; }

    public string ReserverId { get; set; } = null!;

    public int RoomId { get; set; }
    
    public virtual Room Room { get; set; } = null!;
}
