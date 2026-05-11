using AutoMapper;
using HotelSystem.BLL.Entities;
using HotelSystem.BLL.DTOs;

namespace HotelSystem.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Room, RoomDto>()
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasePrice))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.CurrentStatus.ToString()));

        CreateMap<Reservation, ReservationDto>()
            .ForMember(dest => dest.RoomNumber, opt => opt.MapFrom(src => src.Room.RoomNumber));
    }
}