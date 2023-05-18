using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace TaskManagementSystem.Persistence;

public class TaskManagementSystemDbContextFactory : IDesignTimeDbContextFactory<TaskManagementSystemDbContext>
{
    public TaskManagementSystemDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "../../TaskManagementSystem.Api")
                .AddJsonFile("appsettings.json")
                .Build();

        var builder = new DbContextOptionsBuilder<TaskManagementSystemDbContext>();
        var connectionString = configuration.GetConnectionString("TaskManagementSystemConnectionString");

        builder.UseNpgsql(connectionString);

        return new TaskManagementSystemDbContext(builder.Options);
    }
}

