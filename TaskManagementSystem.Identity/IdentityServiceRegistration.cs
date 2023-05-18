using System.Text;
using TaskManagementSystem.Identity.Models;
using TaskManagementSystem.Identity.Services;
using TaskManagementSystem.Application.Contracts.Identity;
using TaskManagementSystem.Application.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TaskManagementSystem.Application.Models.Identity;

namespace TaskManagementSystem.Identity;

public static class IdentityServiceRegistration
{
    public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services,
                                                                  IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<ServerSettings>(configuration.GetSection("ServerSettings"));
        services.AddDbContext<TaskManagementSystemIdentityDbContext>(options =>
        options.UseNpgsql(
            configuration.GetConnectionString("TaskManagementSystemIdentityConnectionString"
            ),
        options => options.MigrationsAssembly(typeof(TaskManagementSystemIdentityDbContext).Assembly.FullName)));

        services.AddIdentity<TaskManagementSystemUser, IdentityRole>()
        .AddEntityFrameworkStores<TaskManagementSystemIdentityDbContext>()
        .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();
        services.Configure<DataProtectionTokenProviderOptions>(opt =>
                        opt.TokenLifespan = TimeSpan.FromHours(2));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration["JwtSettings:Issuer"],
                ValidAudience = configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]))
            };
        });
        return services;
    }
}