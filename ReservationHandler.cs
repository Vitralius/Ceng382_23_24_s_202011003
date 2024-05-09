using System;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Numerics;
using System.Threading;
using System.Globalization;

public static class ReservationHandler
{
    public static void displayWeeklySchedule(List<Room> rooms, List<Reservation> reservations)
    {
        List<Reservation> R = new List<Reservation>();
        for (int i = 0; i < reservations.Count(); i++)
        {
            R.Add(reservations[i]);
        }
        DateTime date= DateTime.Now;
        DateTime[] week= {DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), DateTime.Now.AddDays(5), DateTime.Now.AddDays(6)};
        string[] isreserved = {"available", "available", "available", "available", "available", "available", "available"};
        Thread.CurrentThread.CurrentCulture = new CultureInfo("fr-FR");

        Console.Write("\t");
        for (int i=0; i<7; i++)
        {
            Console.Write($"{week[i].ToShortDateString()}" + "\t");
        }
        
        Console.WriteLine();
        for(int i=0; i<rooms.Count;i++)
        {
            Console.Write(rooms[i].roomName + "\t");
            for(int j=0; j<R.Count ;j++)
            {
                if(R[j].room.roomName == rooms[i].roomName)
                {
                    for(int k=0; k<week.Length; k++)
                    {
                        if(R[j].date.ToShortDateString() == week[k].ToShortDateString())
                        {
                            isreserved[k] = "RESERVED";
                            R.RemoveAt(j);
                            j--;
                            break;
                        }
                    }
                }
            }
        for(int l=0; l<isreserved.Length; l++)
        {
            Console.Write(isreserved[l] + "\t");
        }
        Console.WriteLine();
        for(int m=0; m<isreserved.Length; m++)
        {
            isreserved[m]="available";
        }
        }
    }
}
