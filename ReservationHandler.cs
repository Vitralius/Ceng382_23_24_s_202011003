using System;
using System.Data;
using System.Drawing;
using System.Dynamic;
using System.Numerics;

public static class ReservationHandler
{
    public static void displayWeeklySchedule(List<Room> rooms, List<Reservation> reservations)
    {
        List<Reservation> R = reservations;
        DateTime[] week= {DateTime.Now, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), DateTime.Now.AddDays(4), DateTime.Now.AddDays(5), DateTime.Now.AddDays(6)};
        string[] temp = {"null", "null", "null", "null", "null", "null", "null", };
        string[] reservedate = temp;

        Console.Write(" \t");
        for (int i=0; i<7; i++)
        {
            Console.Write($"{week[i].ToShortDateString()}" + "\t");
        }
        
        Console.WriteLine();
        for(int i=0; i<rooms.Count;i++)
        {
            Console.Write(rooms[i] + "\t");
            for(int j=0; j<R.Count ;j++)
            {
                if(R[j].room == rooms[i])
                {
                    for(int k=0; k<7; k++)
                    {
                        if(R[j].date == week[k])
                        {
                            reservedate[k] = R[j].date.ToShortDateString();
                            R.RemoveAt(j);
                        }
                    }
                }
            }
            for(int l=0; l<7; l++)
            {
                Console.Write(reservedate[l] + "\t");
            }
            Console.WriteLine();
            reservedate = temp;
        }
    }
}
