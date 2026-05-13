namespace HotelSystem.BLL.Entities;

public class Reservation
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
    public string CustomerName { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int StayDurationDays { get; set; }
    public double FinalPrice { get; set; }
}