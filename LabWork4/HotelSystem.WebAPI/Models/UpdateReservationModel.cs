using System.ComponentModel.DataAnnotations;

namespace HotelSystem.WebAPI.Models;

public class UpdateReservationRequest
{
    [Required(ErrorMessage = "Ім'я клієнта є обов'язковим")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Ім'я має містити від 2 до 50 символів")]
    public string CustomerName { get; set; }

    [Required]
    [Range(1, 30, ErrorMessage = "Кількість днів має бути від 1 до 30")]
    public int Days { get; set; }
}