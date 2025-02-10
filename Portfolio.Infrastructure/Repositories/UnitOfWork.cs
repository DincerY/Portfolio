using Microsoft.EntityFrameworkCore;
using Portfolio.Domain.Interfaces.Repositories;

namespace Portfolio.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }
}