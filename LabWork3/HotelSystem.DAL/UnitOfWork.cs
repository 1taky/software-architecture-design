using HotelSystem.BLL.Interfaces;
using HotelSystem.DAL.Repositories;

namespace HotelSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly HotelDbContext _context;

    public UnitOfWork(HotelDbContext context)
    {
        _context = context;
        _context.Database.EnsureCreated(); 
    }

    public IRepository<T> GetRepository<T>() where T : class 
        => new GenericRepository<T>(_context);

    public async Task CommitAsync() => await _context.SaveChangesAsync();

    public void Dispose()
    {
        _context.Dispose();
    }
}