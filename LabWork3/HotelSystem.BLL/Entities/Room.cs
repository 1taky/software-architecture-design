namespace HotelSystem.BLL.Entities;

public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public RoomCategory Category { get; set; }
    public RoomStatus CurrentStatus { get; set; }
    public double BasePrice { get; set; }
}