using UserManagement.Data;
using UserManagement.Repositories.Contracts;

namespace UserManagement.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserContext _context;

    public UnitOfWork(UserContext context)
    {
        _context = context;
    }

    public void SaveChanges()
    {
        _context.SaveChangesAsync();
    }
}