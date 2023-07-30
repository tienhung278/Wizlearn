using System.Collections.Generic;

namespace UserManagement.Models.VMs;

public class FilterUserVM
{
    public string NRIC { get; set; }
    public string Name { get; set; }
    public IEnumerable<UserVM> Users { get; set; }
}