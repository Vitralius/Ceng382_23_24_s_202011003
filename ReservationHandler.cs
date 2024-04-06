using System;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class ReservationHandler
{
    private Reservation[][] reservations {get; set;}
    public void addReservation(Reservation reservation)
    {
        int column = (int) (reservation.date.DayOfWeek);
        int row = reservations.GetLength(0);
        for(int i=0; i<row;i++)
        {
            if(reservations[i][column].reserverName == "null")
            {
                reservations[i][column] = reservation;
            }
        }
    }
    public void deleteReservation (Reservation reservation)
    {
        for(int i=0; i<reservations.GetLength(0); i++)
        {
                for(int j=0; j<reservations[0].GetLength(0); j++)
                {
                    if (reservations[i][j].reserverName == reservation.reserverName && reservations[i][j].room.roomId == reservation.room.roomId)
                    {
                        reservations[i][j] = new Reservation();
                    }
                }
        }        
    }
    public void displayWeeklySchedule(RoomData roomData)
    {
        Console.WriteLine("\t" + "Monday\t Tuesday\t Wednesday\t Thuersday\t Friday\t         Saturday\t Sunday", Color.Green);
        for(int i=0; i<reservations.GetLength(0); i++)
        {
            Console.Write(roomData.Rooms[i].roomName+ "\t");
            for(int j=0; j<reservations[0].GetLength(0); j++)
            {
                Console.Write(reservations[i][j].reserverName + "::" + reservations[i][j].room.roomId + "\t" + " ");
            }
            Console.WriteLine();
        }
    }
    public ReservationHandler()
    {
        reservations = new Reservation[5][];
        for (int i = 0; i < 5; i++)
        {
            reservations[i] = new Reservation[7];
            for (int j = 0; j < 7; j++)
            {
                reservations[i][j] = new Reservation();
            }
        }
    }
}