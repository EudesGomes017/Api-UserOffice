using Data.Mapping;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Context;

public class ApiUserOfficeContext : IdentityDbContext
{
    public ApiUserOfficeContext(DbContextOptions<ApiUserOfficeContext> options) : base(options) { }

    public DbSet<User> User { get; set; }
    public DbSet<Department> Department { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new UserMap());
        modelBuilder.ApplyConfiguration(new DepartmentMap());
    }
}


