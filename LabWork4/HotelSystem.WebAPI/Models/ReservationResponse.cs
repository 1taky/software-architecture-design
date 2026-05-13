namespace HotelSystem.WebAPI.Models;

public class ReservationResponse
{
    public int ReservationId { get; set; }
    public int RoomNumber { get; set; }
    public string Customer { get; set; }
    public double TotalPrice { get; set; }
}