using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Reservation
{
    public int ReservationId { get; set; }

    public DateOnly Date { get; set; }

    public bool IsDeleted { get; set; }

    public bool IsCanceled { get; set; }

    public bool IsConfirmed { get; set; }

    public int RoomId { get; set; }

    public string ReserverId { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
