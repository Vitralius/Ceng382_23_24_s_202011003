using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public static class RoomHandler 
{
    public static Room findRoom(string roomName, List<Room> rooms)
    { 
      Room found = new Room ();      
      for(int i = 0; i < rooms.Count; i++)
        {
            if(rooms[i].roomName== roomName)
            {
               found = rooms[i];
               break;
            }
        }
        return found;
    }
    public static List<Room> GetRooms() 
    {   
        string filePath = "RoomData.json";
        List<Room> R = new List<Room>();
        try //tries to catch errors from file to string phase
        {
            string jsonString = File.ReadAllText(filePath);
            var roomData = JsonSerializer.Deserialize<RoomData>(jsonString, new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
            });
            

            if(roomData?.rooms != null)
            {
                
                for(int i = 0; i < roomData.rooms.Count; i++)
                {
                    R.Add(roomData.rooms[i]);
                }
            }
            else
            {
                Console.WriteLine("roomData is empty!");
            }
            return R;

        }
        catch (Exception e)
        {
            Console.WriteLine($"An unexpected error occurred at GetRooms(): {e.Message}");
            return R;
        }     
    }
    public static void SaveRooms(List<Room> R)
    {
        var roomData = new RoomData(R);
        string jsonString = JsonSerializer.Serialize(roomData, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        });
        File.WriteAllText("RoomData.json", jsonString);
        Console.WriteLine("Rooms saved successfully.");
    }
    public static void ListAvailableRooms(DateTime DT)
    {
        //This will held soon.
    }

}