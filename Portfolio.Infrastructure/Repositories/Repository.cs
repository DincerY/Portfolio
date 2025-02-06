using System.Linq.Expressions;
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

    public TEntity Add(TEntity entity)
    {
        var entry = set.Add(entity);
        _context.SaveChanges();
        return entry.Entity;
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

    public IEnumerable<TEntity> GetAllWithRelation(params Expression<Func<TEntity, object>>[] expressions)
    {
        var query = set.AsQueryable();
        foreach (var expression in expressions)
        {
            query = query.Include(expression);
        }
        return query.ToList();
    }

    public TEntity GetById(int id)
    {
        return set.Find(id);
    }


    //Getirilecek veriye birden fazla tabloyu dahil etmek için yazılan fonksiyon
    //Ayrıca generic yapıyı destekliyor.
    public TEntity GetByIdWithRelation(int id, params Expression<Func<TEntity, object>>[] expressions)
    {
        var query = set.AsQueryable();
        foreach (var exp in expressions)
        {
            query = query.Include(exp);
        }

        return query.FirstOrDefault(art => art.Id == id);
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}