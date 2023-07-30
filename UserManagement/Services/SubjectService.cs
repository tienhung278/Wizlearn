using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Models.VMs;
using UserManagement.Repositories.Contracts;
using UserManagement.Services.Contracts;

namespace UserManagement.Services;

public class SubjectService : ISubjectService
{
    private readonly ISubjectRepository _subjectRepository;

    public SubjectService(IRepositoryManager repositoryManager)
    {
        _subjectRepository = repositoryManager.SubjectRepository;
    }

    public async Task<IEnumerable<SubjectVM>> GetSubjectsAsync()
    {
        var subjects = await _subjectRepository.GetAllAsync();
        var subjectVMs = subjects.Select(s => new SubjectVM(s));
        return subjectVMs;
    }
}