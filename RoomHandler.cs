using System;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;

public static class RoomHandler 
{
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
            

            if(roomData?.Rooms != null)
            {
                for(int i = 0; i < roomData.Rooms.Length; i++)
                {
                    R[i] = roomData.Rooms[i];
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
            Console.WriteLine($"An unexpected error occurred: {e.Message}");
            return R;
        }     
    }
    public static void SaveRooms(List<Room> R)
    {
        string json = JsonSerializer.Serialize(R);
        File.WriteAllText(@"C:\Users\doruk\Desktop\Ceng382_Project\temp\Ceng382_23_24_s_202011003\ReservationData.json", json);
    }
    public static void ListAvailableRooms()
    {

    }

}