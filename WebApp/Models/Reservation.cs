public class Reservation
{
    public int ReservationId { get; set;}
    public DateTime Date {get; set;}
    public string ReserverName {get; set;}
    public Room Room {get; set;}
    public Reservation()
    {
      ReservationId = -1;
      Date = DateTime.Now;
      ReserverName = "null";
      Room = new Room();
    }
}