
namespace Portfolio.Domain.Interfaces.Repositories;

public interface IUnitOfWork
{
    public int SaveChanges();
}