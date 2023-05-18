using TaskManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskManagementSystem.Persistence.Configurations.Entities;

public class CheckListConfiguration: IEntityTypeConfiguration<CheckList>
{
    public void Configure(EntityTypeBuilder<CheckList> builder)
    {
        builder.HasData(
                new CheckList
                {
                    Id=1,
                    UserTaskId=1,
                    Title= "Do something",
                    CreatedAt=DateTime.Now,
                    LastUpdated=DateTime.Now,
                    Description = "this is the first checklist"
                },

                new CheckList
                {
                    Id=2,
                    UserTaskId=1,
                    Title= "Do something",
                    CreatedAt=DateTime.Now,
                    LastUpdated=DateTime.Now,
                    Description = "this is the second checklist"
                }
            );
    }

}

