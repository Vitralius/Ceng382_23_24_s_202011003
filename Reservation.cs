using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class Reservation
{
    public DateTime time {get; set;}
    public DateTime date {get; set;}
    public String reserverName {get; set;}
    public Room room {get; set;}
    public Reservation()
    {
      time = DateTime.Now;
      date = DateTime.Now;
      reserverName = "null";
      room = new Room();
    }
    public Reservation(DateTime T, DateTime D, String RN, Room R)
    {
      time = T;
      date = D;
      reserverName = RN;
      room = R;
    }

}