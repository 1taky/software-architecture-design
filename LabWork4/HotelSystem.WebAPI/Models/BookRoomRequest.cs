using System.ComponentModel.DataAnnotations;

namespace HotelSystem.WebAPI.Models;

public class BookRoomRequest
{
    [Required(ErrorMessage = "ID кімнати є обов'язковим")]
    public int RoomId { get; set; }

    [Required(ErrorMessage = "Ім'я клієнта є обов'язковим")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Ім'я має містити від 2 до 50 символів")]
    public string CustomerName { get; set; }

    [Required]
    [Range(1, 30, ErrorMessage = "Можна забронювати номер на термін від 1 до 30 днів")]
    public int Days { get; set; }
}