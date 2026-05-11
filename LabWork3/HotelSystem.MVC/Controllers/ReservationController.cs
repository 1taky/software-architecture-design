using Microsoft.AspNetCore.Mvc;
using HotelSystem.BLL.Services;
using HotelSystem.BLL.DTOs;

namespace HotelSystem.MVC.Controllers;

public class ReservationsController : Controller
{
    private readonly ReservationService _resService;

    public ReservationsController(ReservationService resService)
    {
        _resService = resService;
    }

    public async Task<IActionResult> Index()
    {
        List<ReservationDto> reservations = await _resService.GetAllReservationsAsync();
        return View(reservations);
    }

    [HttpPost]
    public async Task<IActionResult> CheckIn(int id)
    {
        await _resService.CheckInAsync(id);
        return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> Finish(int id)
    {
        await _resService.FinishReservationAsync(id);
        return RedirectToAction("Index");
    }
}