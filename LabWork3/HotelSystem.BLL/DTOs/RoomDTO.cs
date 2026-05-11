namespace HotelSystem.BLL.DTOs;

public class RoomDto
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string Category { get; set; }
    public string Status { get; set; }
    public double Price { get; set; }
}