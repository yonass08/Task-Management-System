using TaskManagementSystem.Identity.Configurations;
using TaskManagementSystem.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaskManagementSystem.Identity;

public class TaskManagementSystemIdentityDbContext : IdentityDbContext<TaskManagementSystemUser>
{
    public TaskManagementSystemIdentityDbContext(DbContextOptions<TaskManagementSystemIdentityDbContext> options) :
        base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new RoleConfiguration());
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new UserRoleConfiguration());
    }
}