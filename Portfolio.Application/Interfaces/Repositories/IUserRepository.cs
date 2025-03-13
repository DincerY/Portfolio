using Portfolio.Domain.Entities;
using System.Linq.Expressions;

namespace Portfolio.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    User GetByEmail(string email);
    User GetByUsername(string username);


}