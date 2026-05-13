using Microsoft.EntityFrameworkCore;
using HotelSystem.BLL.Entities;
using HotelSystem.BLL.Entities.Enums;

namespace HotelSystem.DAL;

public class HotelDbContext : DbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomNumber = 101, Category = "Standard", BasePrice = 1000.0, CurrentStatus = RoomStatus.Available },
            new Room { Id = 2, RoomNumber = 202, Category = "Lux", BasePrice = 3000.0, CurrentStatus = RoomStatus.Available },
            new Room { Id = 3, RoomNumber = 305, Category = "Presidential", BasePrice = 8000.0, CurrentStatus = RoomStatus.Available }
        );
    }
}