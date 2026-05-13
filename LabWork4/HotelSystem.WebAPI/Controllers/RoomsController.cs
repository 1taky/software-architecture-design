using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using HotelSystem.BLL.Services;
using HotelSystem.WebAPI.Models;
using HotelSystem.BLL.DTOs;

namespace HotelSystem.WebAPI.Controllers;

[ApiController]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    private readonly RoomService _roomService;
    private readonly IMapper _mapper;

    public RoomsController(RoomService roomService, IMapper mapper)
    {
        _roomService = roomService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms([FromQuery] bool onlyAvailable = false)
    {
        List<RoomDto> dtos = await _roomService.GetAllRoomsAsync();
        if (onlyAvailable)
        {
            dtos = dtos.Where(r => r.Status == "Available").ToList();
        }
        List<RoomResponse> responses = _mapper.Map<List<RoomResponse>>(dtos);
        return Ok(responses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        RoomDto dto = await _roomService.GetRoomByIdAsync(id);
        if (dto == null) return NotFound();
        return Ok(_mapper.Map<RoomResponse>(dto));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoomRequest request)
    {
        bool success = await _roomService.CreateRoomAsync(request.RoomNumber, request.Category, request.Price);
        
        if (!success) 
            return BadRequest(new { error = $"Кімната з номером {request.RoomNumber} вже існує!" });
            
        return Ok(new { message = "Кімнату успішно додано" });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoomRequest request)
    {
        bool success = await _roomService.UpdateRoomAsync(id, request.Category, request.Price);
        
        if (!success) 
            return NotFound(new { error = $"Кімнату з ID {id} не знайдено." });
            
        return Ok(new { message = "Дані номера оновлено" });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _roomService.DeleteRoomAsync(id);
        
        if (!success) 
            return BadRequest(new { error = "Не вдалося видалити. Кімната не існує, або вона наразі зайнята/заброньована." });
            
        return Ok(new { message = "Номер успішно видалено з бази" });
    }
}