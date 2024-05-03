using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationData
{
    public const int RESERVATIONSIZE = 10;
    [JsonPropertyName("Reservation")]
    public Reservation[] reservations {get; set;} = new Reservation[RESERVATIONSIZE];
}
public class Reservation
{
    [JsonPropertyName("date")]
    public DateTime date {get; set;}
    [JsonPropertyName("reserverName")]
    public String reserverName {get; set;}
    [JsonPropertyName("room")]
    public Room room {get; set;}
    public Reservation()
    {
      date = DateTime.Now;
      reserverName = "null";
      room = new Room();
    }
    public Reservation(DateTime D, String RN, Room R)
    {
      date = D;
      reserverName = RN;
      room = R;
    }

}