using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationData
{
    public const int RESERVATIONSIZE = 140;
    [JsonPropertyName("Reservaiton")]
    public Reservation[] reservations {get; set;} = new Reservation[RESERVATIONSIZE];
}
public class Reservation
{
    [JsonPropertyName("time")]
    public DateTime time {get; set;}
    [JsonPropertyName("date")]
    public DateTime date {get; set;}
    [JsonPropertyName("reserverName")]
    public String reserverName {get; set;}
    public Room room {get; set;}
    public Reservation()
    {
      time = DateTime.Now;
      date = DateTime.Now;
      reserverName = "Anonymous";
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