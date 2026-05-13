using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HotelSystem.BLL.Services;
using HotelSystem.BLL.DTOs;
using HotelSystem.WebAPI.Models;

namespace HotelSystem.WebAPI.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationsController : ControllerBase
{
    private readonly ReservationService _resService;
    private readonly IMapper _mapper;

    public ReservationsController(ReservationService resService, IMapper mapper)
    {
        _resService = resService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<ReservationDto> dtos = await _resService.GetAllReservationsAsync();
        List<ReservationResponse> responses = _mapper.Map<List<ReservationResponse>>(dtos);
        return Ok(responses);
    }

    [HttpPost]
    public async Task<IActionResult> BookRoom([FromBody] BookRoomRequest request)
    {
        BookingDto result = await _resService.CreateReservationAsync(request.RoomId, request.CustomerName, request.Days);
        if (!result.IsSuccess) return BadRequest(new { error = result.Message });
        return Ok(new { message = result.Message });
    }

    [HttpPut("{id}/checkin")]
    public async Task<IActionResult> CheckIn(int id)
    {
        await _resService.CheckInAsync(id);
        return Ok(new { message = $"Бронювання {id} переведено в статус Occupied." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> FinishReservation(int id)
    {
        bool success = await _resService.DeleteReservationAsync(id);
        if (!success) return NotFound(new { error = "Бронювання не знайдено." });
        
        return Ok(new { message = "Запис про бронювання видалено." });
    }
}