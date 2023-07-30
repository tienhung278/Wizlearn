namespace UserManagement.Services.Contracts;

public interface IServiceManager
{
    IUserService UserService { get; }
    ISubjectService SubjectService { get; }
}