using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class ReservationRepository : IReservationRepository
{
    public List<Reservation> reservations;
    public List<Room> rooms;
    public ReservationRepository()
    {
        reservations = new List<Reservation>();
        rooms = new List<Room>();
        LoadReservations();
        LoadRooms();
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
        try //tries to catch errors from file to string phase
        {
            string jsonString = File.ReadAllText(filePath);
            var reservationData = JsonSerializer.Deserialize<ReservationData>(jsonString, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });
            

            if(reservationData?.reservations != null)
            {
                for(int i = 0; i < reservationData.reservations.Count; i++)
                {
                    reservations.Add(reservationData.reservations[i]);
                }
            }
            else
            {
                Console.WriteLine("reservationData is empty!");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred at LoadReservations(): {e.Message}");
        }  
    }
    public void LoadRooms()
    {
        rooms= RoomHandler.GetRooms();
    }
    public void SaveReservations()
    {
        var reservationData = new ReservationData(reservations);
        string jsonString = JsonSerializer.Serialize(reservationData, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        });
        File.WriteAllText("ReservationData.json", jsonString);
        Console.WriteLine("Reservations saved successfully.");
    }
    public void SaveRooms()
    {
        RoomHandler.SaveRooms(rooms);
    }
    public void AddReservation(Reservation reservation)
    {
        if(!isreserved(reservation))
        {
            reservations.Add(reservation);
        }
        else
        {
            Console.WriteLine("Given values are already reserved pls take another reservartion.");
        }
        
    }
    public void DeleteReservation(Reservation reservation)
    {
        for(int i=0; i<reservations.Count; i++)
        {
            if(reservation.reserverName==reservations[i].reserverName && reservations[i].date.ToShortDateString() == reservation.date.ToShortDateString() && reservation.room.roomName==reservations[i].room.roomName)
            {
                reservations.RemoveAt(i);
                Console.WriteLine("Reservation deleted!");
                break;
            }
        }
    }
    public bool isreserved(Reservation R)
    {
        for(int i=0; i<reservations.Count;i++)
        {
            if(R.date.ToShortDateString()==reservations[i].date.ToShortDateString() && R.room.roomName==reservations[i].room.roomName)
            {
                return true;
            }   
        }
        return false;
    }
}
interface IReservationRepository
{
    public void AddReservation(Reservation reservation);
    public void DeleteReservation(Reservation reservation);
}