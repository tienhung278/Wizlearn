using System;
using System.Data.Entity;
using UserManagement.Models;

namespace UserManagement.Data;

public class UserContext : DbContext
{
    public UserContext()
        : base("name=UserContext")
    {
        Database.SetInitializer(new UserContextInitializer());
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Subject> Subjects { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Properties<DateTime>()
            .Configure(c => c.HasColumnType("datetime2"));
    }
}