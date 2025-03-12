using Portfolio.Application.Interfaces.Repositories;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
}