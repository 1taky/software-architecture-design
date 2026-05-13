using HotelSystem.BLL.Interfaces;
using HotelSystem.BLL.DTOs;
using HotelSystem.BLL.Entities;
using AutoMapper;
using HotelSystem.BLL.Entities.Enums;

namespace HotelSystem.BLL.Services;

public class RoomService
{
    private readonly IRepository<Room> _roomRepo;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RoomService(IRepository<Room> roomRepo, IUnitOfWork uow, IMapper mapper)
    {
        _roomRepo = roomRepo;
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<RoomDto>> GetAllRoomsAsync()
    {
        List<Room> rooms = await _roomRepo.GetAllAsync();
        return _mapper.Map<List<RoomDto>>(rooms);
    }

    public async Task<RoomDto> GetRoomByIdAsync(int id)
    {
        Room room = await _roomRepo.GetByIdAsync(id);
        return _mapper.Map<RoomDto>(room);
    }

    public async Task<bool> CreateRoomAsync(int number, string category, double price)
    {   
        List<Room> allRooms = await _roomRepo.GetAllAsync();
        if (allRooms.Any(r => r.RoomNumber == number))
        {
            return false;
        }

        Room room = new Room 
        { 
            RoomNumber = number, 
            Category = category, 
            BasePrice = price, 
            CurrentStatus = RoomStatus.Available 
        };
        await _roomRepo.CreateAsync(room);
        await _uow.CommitAsync();
        return true;
    }

    public async Task<bool> UpdateRoomAsync(int id, string category, double price)
    {
        Room room = await _roomRepo.GetByIdAsync(id);
        
        if (room == null) return false;

        room.Category = category;
        room.BasePrice = price;
        _roomRepo.Update(room);
        await _uow.CommitAsync();
        return true;
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        Room room = await _roomRepo.GetByIdAsync(id);
        
        if (room == null) return false;
        
        if (room.CurrentStatus != RoomStatus.Available) return false;

        _roomRepo.Delete(room);
        await _uow.CommitAsync();
        return true;
    }

    public async Task UpdateRoomStatusAsync(int roomId, RoomStatus newStatus)
    {
        Room room = await _roomRepo.GetByIdAsync(roomId);
        if (room != null)
        {
            room.CurrentStatus = newStatus;
            _roomRepo.Update(room);
            await _uow.CommitAsync();
        }
    }
}