using Autofac;
using Microsoft.EntityFrameworkCore;
using HotelSystem.DAL;
using HotelSystem.BLL.Interfaces;
using HotelSystem.BLL.Services;
using HotelSystem.DAL.Repositories;

namespace HotelSystem.IoC;

public class HotelAutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        DbContextOptions<HotelDbContext> options = new DbContextOptionsBuilder<HotelDbContext>()
            .UseInMemoryDatabase("HotelStorage")
            .Options;

        builder.Register(c => new HotelDbContext(options)).InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(GenericRepository<>))
               .As(typeof(IRepository<>))
               .InstancePerLifetimeScope();
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<RoomService>().AsSelf().InstancePerLifetimeScope();
        builder.RegisterType<ReservationService>().AsSelf().InstancePerLifetimeScope();
    }
}