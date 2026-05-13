namespace HotelSystem.WebAPI.Models;

public class BookRoomRequest
{
    public int RoomId { get; set; }
    public string CustomerName { get; set; }
    public int Days { get; set; }
}