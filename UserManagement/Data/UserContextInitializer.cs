using System;
using System.Collections.Generic;
using System.Data.Entity;
using UserManagement.Models;

namespace UserManagement.Data;

public class UserContextInitializer : DropCreateDatabaseAlways<UserContext>
{
    protected override void Seed(UserContext context)
    {
        var subjects = new List<Subject>
        {
            new()
            {
                Id = 1,
                Name = "Subject 1"
            },
            new()
            {
                Id = 2,
                Name = "Subject 2"
            },
            new()
            {
                Id = 3,
                Name = "Subject 3"
            }
        };

        context.Subjects.AddRange(subjects);

        base.Seed(context);
    }
}