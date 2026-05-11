using HotelSystem.BLL.Interfaces;
using HotelSystem.BLL.DTOs;
using HotelSystem.BLL.Entities;
using AutoMapper;

namespace HotelSystem.BLL.Services;

public class ReservationService
{
    private readonly IUnitOfWork _uow;
    private readonly RoomService _roomService;
    private readonly IMapper _mapper;

    public ReservationService(IUnitOfWork uow, RoomService roomService, IMapper mapper)
    {
        _uow = uow;
        _roomService = roomService;
        _mapper = mapper;
    }

    public async Task<BookingDto> CreateReservationAsync(int roomId, string customer, int days)
    {
        IRepository<Room> roomRepo = _uow.GetRepository<Room>();
        Room room = await roomRepo.GetByIdAsync(roomId);

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

        await _uow.GetRepository<Reservation>().CreateAsync(res);
        await _uow.CommitAsync();

        return new BookingDto(true, "Успішно заброньовано!");
    }

    public async Task<List<ReservationDto>> GetAllReservationsAsync()
    {
        IRepository<Reservation> repo = _uow.GetRepository<Reservation>();
        List<Reservation> reservations = await repo.GetAllAsync();
        
        IRepository<Room> roomRepo = _uow.GetRepository<Room>();
        foreach (Reservation res in reservations)
        {
            res.Room = await roomRepo.GetByIdAsync(res.RoomId);
        }
        return _mapper.Map<List<ReservationDto>>(reservations);
    }

    public async Task CheckInAsync(int reservationId)
    {
        IRepository<Reservation> resRepo = _uow.GetRepository<Reservation>();
        Reservation res = await resRepo.GetByIdAsync(reservationId);
        if (res != null) await _roomService.UpdateRoomStatusAsync(res.RoomId, RoomStatus.Occupied);
    }

    public async Task FinishReservationAsync(int reservationId)
    {
        IRepository<Reservation> resRepo = _uow.GetRepository<Reservation>();
        Reservation res = await resRepo.GetByIdAsync(reservationId);

        if (res != null)
        {
            await _roomService.UpdateRoomStatusAsync(res.RoomId, RoomStatus.Available);
            resRepo.Delete(res);
            await _uow.CommitAsync();
        }
    }
}