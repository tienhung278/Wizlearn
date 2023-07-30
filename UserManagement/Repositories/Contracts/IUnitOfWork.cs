namespace UserManagement.Repositories.Contracts;

public interface IUnitOfWork
{
    void SaveChanges();
}