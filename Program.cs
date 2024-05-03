using System;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Runtime.InteropServices;

class Program
{
    static void Main(string [] args)
    {
        string checkadd = "y";
        string checkdelete = "n";
        FileLogger fileLogger = new FileLogger();
        ReservationRepository reservationReposiroty = new ReservationRepository();

        reservationReposiroty.LoadReservations();
        reservationReposiroty.LoadRooms();

        Console.WriteLine("Previous weekly schedule:");
        ReservationHandler.displayWeeklySchedule(reservationReposiroty.getRooms(), reservationReposiroty.getReservations());

        do
        {
            Console.WriteLine("Reserver name: ");
            string name = Console.ReadLine() ?? "null";
            Console.WriteLine("Room to reserve: ");
            string room = Console.ReadLine() ?? "null";
            Console.WriteLine("Reservation date (ex: dd.mm.yyyy): ");
            string temp = Console.ReadLine() ?? "01.01.2000";
            DateTime date = DateTime.ParseExact(temp, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            Reservation newReservation = new Reservation(date, name, RoomHandler.findRoom(room,reservationReposiroty.getRooms()));
            reservationReposiroty.AddReservation(newReservation);
            LogRecord newLog = new LogRecord(name, room, DateTime.Now);

            fileLogger.NewLog(newLog);
            LogHandler.AddLog(fileLogger, newLog);

            Console.WriteLine("Enter y to continue to add otherwise enter anything!");
            checkadd = Console.ReadLine() ?? "y";
        }while(checkadd=="y");

        Console.WriteLine("Enter y to delete a reservation otherwise enter anything!");
        checkdelete = Console.ReadLine() ?? "n";

        while(checkdelete=="y")
        {
            Console.WriteLine("Reserver name: ");
            string name = Console.ReadLine() ?? "null";
            Console.WriteLine("Room to reserve: ");
            string room = Console.ReadLine() ?? "null";
            Console.WriteLine("Reservation date (ex: dd.mm.yyyy): ");
            string temp = Console.ReadLine() ?? "01.01.2000";
            DateTime date = DateTime.ParseExact(temp, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            Reservation deleteReservation = new Reservation(date, name, RoomHandler.findRoom(room,reservationReposiroty.getRooms()));
            reservationReposiroty.DeleteReservation(deleteReservation);
            LogRecord newLog = new LogRecord(name, room, DateTime.Now);

            
            
            LogHandler.AddLog(fileLogger, newLog);

            Console.WriteLine("Enter y to continue to delete otherwise enter anything!");
            checkdelete = Console.ReadLine() ?? "y";
        };

        reservationReposiroty.SaveRooms();
        reservationReposiroty.SaveReservations();
        Console.WriteLine("Here is the new  weekly schedule:");
        ReservationHandler.displayWeeklySchedule(reservationReposiroty.getRooms(), reservationReposiroty.getReservations());
    }
}