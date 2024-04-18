using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationRepository
{
    private List<Reservation> reservations;
    private List<Room> rooms;
    ReservationRepository()
    {
        reservations = new List<Reservation>();
        rooms = new List<Room>();
    }
    void loadReservations()
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

        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }  
    }
    void loadRooms()
    {
        this.rooms= RoomHandler.GetRooms();
    }
    void saveReservations()
    {
        string json = JsonSerializer.Serialize(reservations);
        File.WriteAllText(@"C:\Users\doruk\Desktop\Ceng382_Project\temp\Ceng382_23_24_s_202011003\ReservationData.json", json);
    }
    void saveRooms()
    {
        RoomHandler.SaveRooms(rooms);
    }
}
interface IReservationRepository
{
    List<Reservation> getReservations();
    List<Room> getRooms();
    void addReservation(Reservation reservation);
    void deleteReservation(Reservation reservation);
}