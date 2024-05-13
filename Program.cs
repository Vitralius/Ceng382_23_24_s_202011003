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
        int counter = 0;
        FileLogger fileLogger = new FileLogger();
        ReservationRepository reservationRepository = new ReservationRepository();


        Console.WriteLine("Previous weekly schedule:");
        ReservationHandler.displayWeeklySchedule(reservationRepository.getRooms(), reservationRepository.getReservations());

        do
        {
            Console.Write("Reserver name: ");
            string name = Console.ReadLine() ?? "null";
            Console.Write("Room to reserve: ");
            string room = Console.ReadLine() ?? "null";
            Console.Write("Reservation date (ex: dd/mm/yyyy): ");
            string temp = Console.ReadLine() ?? "01/01/2000";
            DateTime date = DateTime.ParseExact(temp, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            reservationRepository.AddReservation(new Reservation(date, name, RoomHandler.findRoom(room,reservationRepository.getRooms())));
            fileLogger.NewLog(new LogRecord(name, room, DateTime.Now));

            counter++;
            if(counter == 1)
            {
                Console.Write("Enter y to continue to add otherwise enter anything!");
                checkadd = Console.ReadLine() ?? "y";
                counter = 0;
            }

        }while(checkadd=="y");

        Console.Write("Enter y to delete a reservation otherwise enter anything!");
        checkdelete = Console.ReadLine() ?? "n";

         while(checkdelete=="y")
        {
            Console.Write("Reserver name: ");
            string name = Console.ReadLine() ?? "null";
            Console.Write("Room to reserve: ");
            string room = Console.ReadLine() ?? "null";
            Console.Write("Reservation date (ex: dd/mm/yyyy): ");
            string temp = Console.ReadLine() ?? "01/01/2000";
            DateTime date = DateTime.ParseExact(temp, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            reservationRepository.DeleteReservation(new Reservation(date, name, RoomHandler.findRoom(room,reservationRepository.getRooms())));
            fileLogger.NewLog(new LogRecord(name+" DELETED", room, DateTime.Now));

            Console.Write("Enter y to continue to delete otherwise enter anything!");
            checkdelete = Console.ReadLine() ?? "y";
        };

        fileLogger.updatelog();
        reservationRepository.SaveReservations();
        Console.WriteLine("Here is the new  weekly schedule:");
        ReservationHandler.displayWeeklySchedule(reservationRepository.getRooms(), reservationRepository.getReservations());
    }
}