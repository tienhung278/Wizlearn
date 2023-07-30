using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace UserManagement.Models.VMs;

public class UserVM
{
    [Display(Name = "S/N")]
    public int Id { get; set; }

    [Required]
    [Display(Name = "NRIC")]
    public string NRIC { get; set; }

    [Required] 
    [Display(Name = "Name")]
    public string Name { get; set; }

    [Required] public Gender Gender { get; set; }

    [Required] 
    [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage = "Date is invalid. Date format: yyyy-mm-dd")]
    [Display(Name = "Birthday (yyyy-mm-dd)")]
    public string Birthday { get; set; }

    public int Age => DateTime.Today.Year - Convert.ToDateTime(Birthday).Year;

    [RegularExpression(@"([12]\d{3}-(0[1-9]|1[0-2])-(0[1-9]|[12]\d|3[01]))", ErrorMessage = "Date is invalid. Date format: yyyy-mm-dd")]
    [Display(Name = "Available Date (yyyy-mm-yy")]
    public string AvailableDate { get; set; }

    public List<SubjectVM> Subjects { get; set; }

    public int NoOfSubjects => Subjects != null ? Subjects.Count : 0;

    public string SubjectList => string.Join(",", Subjects.Select(s => s.Name));
    
    public UserVM()
    {
    }

    public UserVM(User user)
    {
        Id = user.Id;
        NRIC = user.NRIC;
        Name = user.Name;
        Gender = user.Gender;
        Birthday = user.Birthday.ToString("yyyy-MM-dd");
        AvailableDate = user.AvailableDate?.ToString("yyyy-MM-dd");
        Subjects = user.SubjectIds.Split(',')
            .Where(i => !string.IsNullOrEmpty(i))
            .Select(id => new SubjectVM { Id = Convert.ToInt32(id) })
            .ToList();
    }

    public User ToUser()
    {
        return new User
        {
            Id = Id,
            NRIC = NRIC,
            Name = Name,
            Gender = Gender,
            Birthday = Convert.ToDateTime(Birthday),
            AvailableDate = !string.IsNullOrEmpty(AvailableDate) ? Convert.ToDateTime(AvailableDate) : null,
            SubjectIds = string.Join(",", Subjects.Where(s => s.IsChecked)
                .Select(s => s.Id))
        };
    }
}