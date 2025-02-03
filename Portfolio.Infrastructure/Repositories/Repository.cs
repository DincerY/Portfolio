using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    }

    public DbSet<TEntity> set => _context.Set<TEntity>();

    public int Add(TEntity entity)
    {
        set.Add(entity);
        return _context.SaveChanges();
    }

    public int Delete(int id)
    {
        throw new NotImplementedException();
    }

    public int Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public int AddAll(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetAll()
    {
        return set.ToList();
    }

    public TEntity GetById(int id)
    {
        return set.Find(id);
    }
}