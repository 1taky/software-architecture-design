using AutoMapper;
using HotelSystem.BLL.DTOs;
using HotelSystem.WebAPI.Models;

namespace HotelSystem.WebAPI;

public class ApiMappingProfile : Profile
{
    public ApiMappingProfile()
    {
        CreateMap<RoomDto, RoomResponse>()
            .ForMember(dest => dest.Number, opt => opt.MapFrom(src => src.RoomNumber))
            .ForMember(dest => dest.PricePerNight, opt => opt.MapFrom(src => src.Price));

        CreateMap<ReservationDto, ReservationResponse>()
            .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.CustomerName))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.FinalPrice));
    }
}