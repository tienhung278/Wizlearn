using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Models.VMs;

namespace UserManagement.Services.Contracts;

public interface ISubjectService
{
    Task<IEnumerable<SubjectVM>> GetSubjectsAsync();
}