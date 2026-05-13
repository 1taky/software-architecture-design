using HotelSystem.BLL.Entities.Enums;

namespace HotelSystem.BLL.Entities;

public class Room
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string Category { get; set; }
    public double BasePrice { get; set; }
    public RoomStatus CurrentStatus { get; set; }
}