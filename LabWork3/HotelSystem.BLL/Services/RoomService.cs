using HotelSystem.BLL.Interfaces;
using HotelSystem.BLL.DTOs;
using HotelSystem.BLL.Entities;
using AutoMapper;

namespace HotelSystem.BLL.Services;

public class RoomService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public RoomService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<List<RoomDto>> GetRoomsAsync(bool onlyAvailable = false)
{
    IRepository<Room> repo = _uow.GetRepository<Room>();
    List<Room> rooms = await repo.GetAllAsync();

    if (onlyAvailable)
    {
        rooms = rooms.Where(r => r.CurrentStatus == RoomStatus.Available).ToList();
    }

    return _mapper.Map<List<RoomDto>>(rooms);
}
    public async Task UpdateRoomStatusAsync(int roomId, RoomStatus newStatus)
    {
        IRepository<Room> repo = _uow.GetRepository<Room>();
        Room room = await repo.GetByIdAsync(roomId);
        if (room != null)
        {
            room.CurrentStatus = newStatus;
            repo.Update(room);
            await _uow.CommitAsync();
        }
    }
}