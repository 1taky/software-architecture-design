using HotelSystem.BLL.Interfaces;

namespace HotelSystem.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly HotelDbContext _context;

    public UnitOfWork(HotelDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated(); 
    }

    public async Task CommitAsync() => await _context.SaveChangesAsync();
}