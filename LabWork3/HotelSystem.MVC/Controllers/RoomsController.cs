using Microsoft.AspNetCore.Mvc;
using HotelSystem.BLL.Services;
using HotelSystem.BLL.DTOs;

namespace HotelSystem.MVC.Controllers;

public class RoomsController : Controller
{
    private readonly RoomService _roomService;
    private readonly ReservationService _resService;

    public RoomsController(RoomService roomService, ReservationService resService)
    {
        _roomService = roomService;
        _resService = resService;
    }

    public async Task<IActionResult> Index(bool onlyAvailable = false)
{
    List<RoomDto> rooms = await _roomService.GetRoomsAsync(onlyAvailable);
    
    ViewBag.OnlyAvailable = onlyAvailable; 
    
    return View(rooms);
}

    [HttpPost]
    public async Task<IActionResult> Book(int roomId, string customerName, int days)
    {
        BookingDto result = await _resService.CreateReservationAsync(roomId, customerName, days);
        
        TempData["Message"] = result.Message; 
        
        return RedirectToAction("Index");
    }
}