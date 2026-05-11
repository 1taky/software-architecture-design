using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HotelSystem.DAL;
using HotelSystem.BLL.Interfaces;
using HotelSystem.BLL.Services;
using HotelSystem.BLL;

namespace HotelSystem.IoC;

public static class Container
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<HotelDbContext>(options =>
            options.UseInMemoryDatabase("HotelStorage"));

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<RoomService>();
        services.AddScoped<ReservationService>();
    }
}