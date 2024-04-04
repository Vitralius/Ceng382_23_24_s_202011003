using System;
using System.Text.Json;
using System.Text.Json.Serialization;

class Program
{
    static void Main(String[]args)
    {
        //define file path
        string filePath = "Data.json";

        //Read from json
        //1 -> jston to text // to do try-catch
        string jsonString = File.ReadAllText(filePath);
        //2 -> decode text into meaningful classes
        var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, new JsonSerializerOptions()
        {
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        });

        if(roomData?.Rooms != null)
        {
            foreach (var room in roomData.Rooms)
            {
                Console.WriteLine($"Room ID: {room.roomName} RoomName: {room.roomName} Capacity: {room.capacity}");
            };
        }
    }
}