using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationHandler
{
    private Reservation[,] reservations {get; set;}
    public void addReservation(Reservation reservation)
    {
        int rows = reservations.GetLength(0);
        int columns = reservations.GetLength(1);
        for(int i=0; i<reservations.GetLength(0); i++)
        {
                for(int j=0; j<reservations.GetLength(1); j++)
                {
                        reservations[rows, columns] = reservation;
                }
        }
    }
    public void deleteReservation (Reservation reservation)
    {
        int rows = reservations.GetLength(0);
        int columns = reservations.GetLength(1);
        for(int i=0; i<reservations.GetLength(0); i++)
        {
                for(int j=0; j<reservations.GetLength(1); j++)
                {
                    if (reservations[i,j].room == reservations[i,j] && reservations.date == reservations[i,j] && reservations.reserverName == reservations[i,j])
                        reservations[rows, columns] = reservation;
                }
        }        
    }
    public void displayWeeklySchedule()
    {
        for(int i=0; i<reservations.GetLength(0); i++)
        {
            for(int j=0; j<reservations.GetLength(1); j++)
            {
                Console.WriteLine();
            }
        }
    }
    bool isReserved(Room R, DateTime D)
    {
        for(int i=0; i<reservations.GetLength(0); i++)
        {
            for(int j=0; j<reservations.GetLength(1); j++)
            {
                if(reservations[i,j].room == R && reservations[i,j].date.DayOfWeek == D.DayOfWeek)
                {
                    return true;
                }
            }
        }
        return false;
    }
    public ReservationHandler()
    {
      reservations = new Reservation[RoomData.ROOMSIZE,7];
    }
}