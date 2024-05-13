public class Room
{
public int Id { get; set; }
public string RoomName { get; set; }
public int Capacity { get; set; }
public Room ()
{
    Id = 0;
    RoomName = "";
    Capacity = 0;
}
}