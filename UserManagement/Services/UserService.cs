using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models;
using UserManagement.Models.VMs;
using UserManagement.Repositories.Contracts;
using UserManagement.Services.Contracts;

namespace UserManagement.Services;

public class UserService : IUserService
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public UserService(IRepositoryManager repositoryManager)
    {
        _userRepository = repositoryManager.UserRepository;
        _subjectRepository = repositoryManager.SubjectRepository;
        _unitOfWork = repositoryManager.UnitOfWork;
    }

    public async Task<IEnumerable<UserVM>> GetUsersAsync(FilterUserVM filterUserVm)
    {
        IEnumerable<UserVM> userVMs = null;
        IEnumerable<User> users = null;

        if (filterUserVm == null ||
            (string.IsNullOrEmpty(filterUserVm.NRIC) && string.IsNullOrEmpty(filterUserVm.Name)))
        {
            users = await _userRepository.GetAllAsync();
            userVMs = users.Select(u => new UserVM(u));
        }
        else
        {
            if (!string.IsNullOrEmpty(filterUserVm.NRIC) && !string.IsNullOrEmpty(filterUserVm.Name))
                users = await _userRepository.GetAsync(u =>
                    u.NRIC.Contains(filterUserVm.NRIC) && u.Name.Contains(filterUserVm.Name));
            else
                users = await _userRepository.GetAsync(u =>
                    u.NRIC.Contains(filterUserVm.NRIC) || u.Name.Contains(filterUserVm.Name));
            userVMs = users.Select(u => new UserVM(u));
        }

        return userVMs;
    }

    public async Task<UserVM> GetUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return new UserVM(user);
    }

    public async Task<int> CreateUserAsync(UserVM userVm)
    {
        var user = userVm.ToUser();
        await _userRepository.AddAsync(user);
        _unitOfWork.SaveChanges();
        return user.Id;
    }

    public async Task UpdateUserAsync(UserVM userVm)
    {
        var user = userVm.ToUser();
        await _userRepository.UpdateAsync(user);
        _unitOfWork.SaveChanges();
    }

    public async Task DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) throw new ArgumentException("Id was not found");
        await _userRepository.DeleteAsync(user);
        _unitOfWork.SaveChanges();
    }
}