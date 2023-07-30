using System;

namespace UserManagement.Models;

public class User : ModelBase
{
    public string NRIC { get; set; }
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime? AvailableDate { get; set; }
    public string SubjectIds { get; set; }
}