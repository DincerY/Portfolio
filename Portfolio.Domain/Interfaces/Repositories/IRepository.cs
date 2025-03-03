using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> set { get; }
    TEntity Add(TEntity entity);
    int Delete(int id);
    bool IsExists(Func<TEntity,bool> predicate);
    int Update(TEntity entity);
    int AddAll(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> GetWhere(params Expression<Func<TEntity, bool>>[] predicate);
    IQueryable<TEntity> GetQueryable();

    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAllWithRelation(params Expression<Func<TEntity, object>>[] expressions);

    TEntity GetById(int id);
    IEnumerable<TEntity> GetByIds(List<int> ids);
    IEnumerable<TEntity> GetByIdWithRelation(int id, params Expression<Func<TEntity, object>>[] expression);

}