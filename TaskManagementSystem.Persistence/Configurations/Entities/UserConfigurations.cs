using TaskManagementSystem.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace TaskManagementSystem.Persistence.Configurations.Entities;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasData(
                new User
                {
                    Id=1,
                    FullName="abebe",
                    Email="abebe@gmail.com"
                },

                new User
                {
                    Id=2,
                    FullName="kebede",
                    Email="kebede@gmail.com"
                }
            );
    }   

}

