using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Repositories.Contracts;

namespace UserManagement.Repositories;

public class SubjectRepository : RepositoryBase<Subject>, ISubjectRepository
{
    public SubjectRepository(UserContext context) : base(context)
    {
    }
}