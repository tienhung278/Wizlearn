using System;
using UserManagement.Repositories;
using UserManagement.Repositories.Contracts;
using UserManagement.Services.Contracts;

namespace UserManagement.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<ISubjectService> _lazySubjectService;
    private readonly Lazy<IUserService> _lazyUserService;

    public ServiceManager()
    {
        IRepositoryManager repositoryManager = new RepositoryManager();
        _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager));
        _lazySubjectService = new Lazy<ISubjectService>(() => new SubjectService(repositoryManager));
    }

    public IUserService UserService => _lazyUserService.Value;
    public ISubjectService SubjectService => _lazySubjectService.Value;
}