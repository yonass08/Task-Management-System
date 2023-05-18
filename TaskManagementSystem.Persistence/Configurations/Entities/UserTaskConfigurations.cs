using TaskManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskManagementSystem.Persistence.Configurations.Entities;

public class UserTaskConfiguration: IEntityTypeConfiguration<UserTask>
{
    public void Configure(EntityTypeBuilder<UserTask> builder)
    {
        builder.HasData(
                new UserTask
                {
                    Id=1,
                    UserId=1,
                    Title= "Do something",
                    CreatedAt=DateTime.Now,
                    LastUpdated=DateTime.Now,
                    Description = "this is the first UserTask",
                    StartDate=DateTime.Now,
                    EndDate=DateTime.MaxValue
                },

                new UserTask
                {
                    Id=2,
                    UserId=1,
                    Title= "Do something",
                    CreatedAt=DateTime.Now,
                    LastUpdated=DateTime.Now,
                    Description = "this is the first UserTask",
                    StartDate=DateTime.Now,
                    EndDate=DateTime.MaxValue
                }
            );
    }

}

