using Microsoft.EntityFrameworkCore;
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
    //TODO
    public int Delete(int id)
    {
        throw new NotImplementedException();
    }
    //TODO

    public int Update(TEntity entity)
    {
        throw new NotImplementedException();
    }
    //TODO

    public int AddAll(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<TEntity> GetWhere(Func<TEntity, bool> predicate)
    {
        return set.Where(predicate);
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return set.AsQueryable();
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