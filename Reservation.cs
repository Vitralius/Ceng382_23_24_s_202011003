using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Reservation
{
    public DateTime time {get; set;}
    public DateTime() date {get; set;}
    public String reserverName {get; set;}
    public Room room {get; set;}
    public Reservation()
    {
      time = DateTime.Now;
      date = new DateTime();
      reserverName = "Null";
      room = new Room();
    }

}