namespace UserManagement.Repositories.Contracts;

public interface IRepositoryManager
{
    public IUserRepository UserRepository { get; }
    public ISubjectRepository SubjectRepository { get; }
    public IUnitOfWork UnitOfWork { get; }
}