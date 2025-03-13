using Portfolio.Application.Interfaces.Repositories;
using Portfolio.CrossCuttingConcerns.Exceptions;
using Portfolio.Domain.Entities;
using Portfolio.Infrastructure.Contexts;

namespace Portfolio.Infrastructure.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public User GetByEmail(string email)
    {
        return set.SingleOrDefault(user => user.Email == email);
    }

    public User GetByUsername(string username)
    {
        return set.SingleOrDefault(user => user.Username == username);
    }
}