using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class RoomLog
{
    public int LogId { get; set; }

    public string Status { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public string UserId { get; set; } = null!;

    public int RoomId { get; set; }

    public DateTime LogDate { get; set; }
}
