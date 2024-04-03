using System;
using System.Dynamic;
using System.Text.Json;
using System.Text.Json.Serialization;


public class RoomData
{
    public const int ROOMSIZE = 20;
    [JsonPropertyName("Room")]
    public Room[] Rooms {get; set;} = new Room[ROOMSIZE];
}
public class Room
{
    [JsonPropertyName("roomId")]
    public string roomId {get; set;}
    [JsonPropertyName("roomName")]
    public string roomName {get; set;}
    [JsonPropertyName("capacity")]
    public int capacity {get; set;}
    public Room()
    {
      roomId = "0";
      roomName = "Null";
      capacity = 0;
    }

}