using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Domain;


namespace TaskManagementSystem.Persistence;

public class TaskManagementSystemDbContext : DbContext
{
    
    public DbSet<UserTask> UserTasks { get; set; }
    public DbSet<CheckList> CheckLists { get; set; }

    
    public TaskManagementSystemDbContext(DbContextOptions<TaskManagementSystemDbContext> options)
        : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        // AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ChangeTracker.LazyLoadingEnabled = false;

        modelBuilder.Entity<UserTask>()
        .HasMany(u => u.CheckLists)
        .WithOne(c => c.UserTask)
        .HasForeignKey(c => c.UserTaskId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TaskManagementSystemDbContext).Assembly);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        foreach (var entry in ChangeTracker.Entries<BaseDomainEntity>())
        {
            entry.Entity.LastUpdated = DateTime.UtcNow;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }


}

