using TaskManagementSystem.Application.Contracts.Persistence;
using TaskManagementSystem.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace TaskManagementSystem.Persistence
{
    public static class PersistenceServicesRegistration
    {
        public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaskManagementSystemDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("TaskManagementSystemConnectionString")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserTaskRepository, UserTaskRepository>();
            services.AddScoped<ICheckListRepository, CheckListRepository>();
            
            return services;
        }
    }
}
