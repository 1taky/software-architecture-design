using HotelSystem.BLL.Interfaces;
using HotelSystem.BLL.DTOs;
using HotelSystem.BLL.Entities;
using AutoMapper;
using HotelSystem.BLL.Entities.Enums;

namespace HotelSystem.BLL.Services;

public class ReservationService
{
    private readonly IUnitOfWork _uow;
    private readonly RoomService _roomService;
    private readonly IMapper _mapper;
    private readonly IRepository<Reservation> _reservationRepo;
    private readonly IRepository<Room> _roomRepo;

    public ReservationService(IRepository<Reservation> reservationRepo, IRepository<Room> roomRepo, IUnitOfWork uow, RoomService roomService, IMapper mapper)
    {
        _reservationRepo = reservationRepo;
        _roomRepo = roomRepo;
        _uow = uow;
        _roomService = roomService;
        _mapper = mapper;
    }

    public async Task<BookingDto> CreateReservationAsync(int roomId, string customer, int days)
    {
        Room room = await _roomRepo.GetByIdAsync(roomId);

        if (room == null || room.CurrentStatus != RoomStatus.Available)
            return new BookingDto(false, "Бронювання неможливе: номер недоступний.");

        await _roomService.UpdateRoomStatusAsync(roomId, RoomStatus.Reserved);

        Reservation res = new Reservation
        {
            RoomId = roomId,
            CustomerName = customer,
            ArrivalDate = DateTime.Now,
            StayDurationDays = days,
            FinalPrice = room.BasePrice * days
        };

        await _reservationRepo.CreateAsync(res);
        await _uow.CommitAsync();

        return new BookingDto(true, "Успішно заброньовано!");
    }

    public async Task<List<ReservationDto>> GetAllReservationsAsync()
    {
        List<Reservation> reservations = await _reservationRepo.GetAllAsync();
        
        foreach (Reservation res in reservations)
        {
            res.Room = await _roomRepo.GetByIdAsync(res.RoomId);
        }
        return _mapper.Map<List<ReservationDto>>(reservations);
    }

    public async Task<ReservationDto> GetReservationByIdAsync(int id)
    {
        Reservation res = await _reservationRepo.GetByIdAsync(id);
        if (res != null) 
        {
            res.Room = await _roomRepo.GetByIdAsync(res.RoomId);
        }
        return _mapper.Map<ReservationDto>(res);
    }

    public async Task UpdateReservationAsync(int id, string customer, int days)
    {
        Reservation res = await _reservationRepo.GetByIdAsync(id);
        if (res != null)
        {
            res.CustomerName = customer;
            res.StayDurationDays = days;
            
            Room room = await _roomRepo.GetByIdAsync(res.RoomId);
            if (room != null)
            {
                res.FinalPrice = room.BasePrice * days;
            }
            
            _reservationRepo.Update(res);
            await _uow.CommitAsync();
        }
    }

    public async Task<bool> DeleteReservationAsync(int id)
    {
        Reservation res = await _reservationRepo.GetByIdAsync(id);
        
        if (res == null) return false;

        await _roomService.UpdateRoomStatusAsync(res.RoomId, RoomStatus.Available);
        _reservationRepo.Delete(res);
        await _uow.CommitAsync();
        return true;
    }

    public async Task CheckInAsync(int reservationId)
    {
        Reservation res = await _reservationRepo.GetByIdAsync(reservationId);
        if (res != null) await _roomService.UpdateRoomStatusAsync(res.RoomId, RoomStatus.Occupied);
    }
}