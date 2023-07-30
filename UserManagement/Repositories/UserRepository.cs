using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Repositories.Contracts;

namespace UserManagement.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(UserContext context) : base(context)
    {
    }
}