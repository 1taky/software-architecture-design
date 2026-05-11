using Microsoft.EntityFrameworkCore;
using HotelSystem.BLL.Interfaces;

namespace HotelSystem.DAL.Repositories;

public class GenericRepository<T> : IRepository<T> where T : class
{

    private readonly DbSet<T> _dbSet;

    public GenericRepository(HotelDbContext context)
    {
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
    public async Task CreateAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Update(T entity) => _dbSet.Update(entity);
    public void Delete(T entity) => _dbSet.Remove(entity);
}