using Portfolio.Domain.Entities;
using Portfolio.Domain.Interfaces.Repositories;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public class AuthorRepository : Repository<Author>,IAuthorRepository
{
    public AuthorRepository(AppDbContext context) : base(context)
    {
    }
}