namespace HotelSystem.BLL.DTOs;

public class ReservationDto
{
    public int Id { get; set; }
    public int RoomNumber { get; set; }
    public string CustomerName { get; set; }
    public DateTime ArrivalDate { get; set; }
    public int StayDurationDays { get; set; }
    public double FinalPrice { get; set; }
}