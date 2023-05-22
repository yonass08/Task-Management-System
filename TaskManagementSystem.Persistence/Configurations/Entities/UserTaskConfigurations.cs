using TaskManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskManagementSystem.Persistence.Configurations.Entities;

public class UserTaskConfiguration : IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.HasData(
                new UserTask
                {
                    Id = 1,
                    UserId = "efa06a55-d0cc-4e01-abbf-870f21d91441",
                    Title = "Do something",
                    CreatedAt = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Description = "this is the first UserTask",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.MaxValue
                },

                new UserTask
                {
                    Id = 2,
                    UserId = "efa06a55-d0cc-4e01-abbf-870f21d91441",
                    Title = "Do something",
                    CreatedAt = DateTime.Now,
                    LastUpdated = DateTime.Now,
                    Description = "this is the first UserTask",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.MaxValue
                }
            );
    }

}

