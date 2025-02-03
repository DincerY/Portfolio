using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> set { get; }
    int Add(TEntity entity);
    int Delete(int id);
    int Update(TEntity entity);
    int AddAll(IEnumerable<TEntity> entities);

    IEnumerable<TEntity> GetAll();
    TEntity GetById(int id);
}