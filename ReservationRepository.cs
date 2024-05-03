using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationRepository : IReservationRepository
{
    private List<Reservation> reservations;
    private List<Room> rooms;
    public ReservationRepository()
    {
        reservations = new List<Reservation>();
        rooms = new List<Room>();
        LoadRooms();
        LoadReservations();
    }
    public List<Reservation> getReservations()
    {
        return reservations;
    }
    public List<Room> getRooms()
    {
        return rooms;
    }

    public void LoadReservations()
    {
        string filePath = "ReservationData.json";
        List<Reservation> R = new List<Reservation>();
        try //tries to catch errors from file to string phase
        {
            string jsonString = File.ReadAllText(filePath);
            var reservationData = JsonSerializer.Deserialize<ReservationData>(jsonString, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });
            

            if(reservationData?.reservations != null)
            {
                for(int i = 0; i < reservationData.reservations.Length; i++)
                {
                    R[i] = reservationData.reservations[i];
                }
            }
            else
            {
                Console.WriteLine("reservationData is empty!");
            }
            this.reservations = R;

        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }  
    }
    public void LoadRooms()
    {
        this.rooms= RoomHandler.GetRooms();
    }
    public void SaveReservations()
    {
        string json = JsonSerializer.Serialize(reservations);
        File.WriteAllText(@"C:\Users\doruk\Desktop\Ceng382_Project\temp\Ceng382_23_24_s_202011003\ReservationData.json", json);
    }
    public void SaveRooms()
    {
        RoomHandler.SaveRooms(rooms);
    }
    public void AddReservation(Reservation reservation)
    {
        reservations.Add(reservation);
    }
    public void DeleteReservation(Reservation reservation)
    {
        reservations.Remove(reservation);
    }
}
interface IReservationRepository
{
    public void AddReservation(Reservation reservation);
    public void DeleteReservation(Reservation reservation);
}