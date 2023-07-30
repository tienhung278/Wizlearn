using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models.VMs;

namespace UserManagement.Services.Contracts;

public interface IUserService
{
    Task<IEnumerable<UserVM>> GetUsersAsync(FilterUserVM filterUserVm);
    Task<UserVM> GetUserAsync(int id);
    Task<int> CreateUserAsync(UserVM userVm);
    Task UpdateUserAsync(UserVM userVm);
    Task DeleteUserAsync(int id);
}