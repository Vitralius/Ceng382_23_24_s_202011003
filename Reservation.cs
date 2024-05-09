using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationData{
    [JsonPropertyName("Reservation")]
    public List<Reservation> reservations { get; set; }
    public ReservationData(List<Reservation> R) { reservations = R; }
    public ReservationData() { reservations = new List<Reservation>();}
}
public class Reservation
{
    [JsonPropertyName("date")]
    public DateTime date {get; set;}
    [JsonPropertyName("reserverName")]
    public string reserverName {get; set;}
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

    public Reservation(Reservation R)
    {
      this.date = R.date;
      this.reserverName = R.reserverName;
      this.room = R.room;
    }

}