using Microsoft.EntityFrameworkCore;
using HotelSystem.BLL.Entities;

namespace HotelSystem.DAL;

public class HotelDbContext : DbContext
{
    public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }
    
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Room>().HasData(
            new Room { Id = 1, RoomNumber = 101, Category = RoomCategory.Standard, CurrentStatus = RoomStatus.Available, BasePrice = 1000 },
            new Room { Id = 2, RoomNumber = 217, Category = RoomCategory.Standard, CurrentStatus = RoomStatus.Available, BasePrice = 1000 },
            new Room { Id = 3, RoomNumber = 301, Category = RoomCategory.Comfort, CurrentStatus = RoomStatus.Available, BasePrice = 2500 },
            new Room { Id = 4, RoomNumber = 808, Category = RoomCategory.Luxury, CurrentStatus = RoomStatus.Available, BasePrice = 5000 }
        );
    }
}