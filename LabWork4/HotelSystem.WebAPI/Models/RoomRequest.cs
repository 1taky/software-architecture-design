using System.ComponentModel.DataAnnotations;

namespace HotelSystem.WebAPI.Models;

public class RoomRequest
{
    [Required]
    [Range(1, 9999, ErrorMessage = "Номер кімнати має бути більше нуля")]
    public int RoomNumber { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 3, ErrorMessage = "Категорія має містити від 3 до 30 символів")]
    public string Category { get; set; }

    [Required]
    [Range(100.0, 100000.0, ErrorMessage = "Ціна має бути в межах від 100 до 100 000")]
    public double Price { get; set; }
}