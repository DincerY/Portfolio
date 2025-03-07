using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Entities;

namespace Portfolio.Domain.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    public DbSet<TEntity> set { get; }
    TEntity Add(TEntity entity);
    TEntity Delete(int id);
    bool IsExists(Func<TEntity,bool> predicate);
    TEntity Update(TEntity entity);
    IEnumerable<TEntity> AddAll(IEnumerable<TEntity> entities);
    IEnumerable<TEntity> GetWhere(params Expression<Func<TEntity, bool>>[] predicate);
    IQueryable<TEntity> GetQueryable();

    IEnumerable<TEntity> GetAll();
    IEnumerable<TEntity> GetAllWithRelation(params Expression<Func<TEntity, object>>[] expressions);

    TEntity GetById(int id);
    IEnumerable<TEntity> GetByIds(List<int> ids);
    TEntity GetByIdWithRelation(int id, params Expression<Func<TEntity, object>>[] expression);
    IEnumerable<TEntity> GetAllWithPagination(int pageSize, int pageNumber);

}