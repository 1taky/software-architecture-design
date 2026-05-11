namespace HotelSystem.BLL.DTOs;

public class BookingDto
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public BookingDto(bool isSuccess, string msg) { IsSuccess = isSuccess; Message = msg; }
}