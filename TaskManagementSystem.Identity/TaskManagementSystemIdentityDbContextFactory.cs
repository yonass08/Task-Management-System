using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TaskManagementSystem.Identity;


public class TaskManagementSystemIdentityDbContextFactory : IDesignTimeDbContextFactory<TaskManagementSystemIdentityDbContext>
{
    public TaskManagementSystemIdentityDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory() + "../../TaskManagementSystem.Api")
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<TaskManagementSystemIdentityDbContext>();
        var connectionString = configuration.GetConnectionString("TaskManagementSystemIdentityConnectionString");

        builder.UseNpgsql(connectionString);

        return new TaskManagementSystemIdentityDbContext(builder.Options);
    }
}
