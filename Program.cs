using System;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main(String[]args)
    {
        string filePath = "Data.json";
        DateTime today = new DateTime(2024,4,5);
        DateTime tomorrow = new DateTime(2024,4,6);
        DateTime yesterday = new DateTime(2024,4,4);
        DateTime[] timearray = {today, yesterday, yesterday, tomorrow, tomorrow}; //E.g times for reservations
        Reservation[] reservations = new Reservation[5]; //Expected reservations
        ReservationHandler Handler = new ReservationHandler(); //Reservation handler
        int counter=0;
        
        try //tries to catch errors from file to string phase to end of process
        {
            string jsonString = File.ReadAllText(filePath);
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });

            if(roomData?.Rooms != null)
            {
                foreach (var room in roomData.Rooms)
                {
                    Console.WriteLine($"Room ID: {room.roomName} RoomName: {room.roomName} Capacity: {room.capacity}");
                    counter++;
                };
                if(counter<=5)
                {
                    for(int i=0; i<counter; i++)
                    {
                        reservations[i] = new Reservation(timearray[i], timearray[i], "Anonymous", roomData.Rooms[i]);
                    }
                }
                else if(counter<=0)
                {
                    Console.WriteLine("There must be at least 1 room in Data.json file!");
                }
                else
                {
                    Console.WriteLine($"There must be less or equal to 5 room in Data.json file! Counter: {counter}");
                }
                Console.WriteLine("OLD: ");
                Handler.displayWeeklySchedule();
                for(int i=0; i<5; i++)
                {
                    Handler.addReservation(reservations[i]);
                }
                Console.WriteLine("NEW:");
                Handler.displayWeeklySchedule();
            }
            else
            {
                Console.WriteLine("roomData is empty!");
            }

        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
        }

    }
}