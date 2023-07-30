using System;
using UserManagement.Data;
using UserManagement.Repositories.Contracts;

namespace UserManagement.Repositories;

public class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<ISubjectRepository> _lazySubjectRepository;
    private readonly Lazy<IUnitOfWork> _lazyUnitOfWork;
    private readonly Lazy<IUserRepository> _lazyUserRepository;

    public RepositoryManager()
    {
        var userContext = new UserContext();
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(userContext));
        _lazySubjectRepository = new Lazy<ISubjectRepository>(() => new SubjectRepository(userContext));
        _lazyUnitOfWork = new Lazy<IUnitOfWork>(() => new UnitOfWork(userContext));
    }

    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public ISubjectRepository SubjectRepository => _lazySubjectRepository.Value;
    public IUnitOfWork UnitOfWork => _lazyUnitOfWork.Value;
}