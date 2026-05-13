namespace HotelSystem.BLL.Interfaces;

public interface IUnitOfWork
{
    Task CommitAsync();
}