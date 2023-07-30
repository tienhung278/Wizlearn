using System.Collections.Generic;
using System.Linq;

namespace UserManagement.Models.VMs;

public class SubjectVM
{
    public SubjectVM()
    {
    }

    public SubjectVM(Subject subject)
    {
        Id = subject.Id;
        Name = subject.Name;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsChecked { get; set; }
    public ICollection<UserVM> Users { get; set; }

    public Subject ToSubject()
    {
        return new Subject
        {
            Id = Id,
            Name = Name
        };
    }
}