namespace HotelSystem.WebAPI.Models;

public class RoomResponse
{
    public int Number { get; set; }
    public string Category { get; set; }
    public double PricePerNight { get; set; }
    public string Status { get; set; }
}