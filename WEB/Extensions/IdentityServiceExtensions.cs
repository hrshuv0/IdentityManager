using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace WEB.Extensions;

public static class IdentityServiceExtensions
{
    public static void AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddIdentity<IdentityUser, IdentityRole>(options =>
        {
            // Password configuration
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 4;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredUniqueChars = 1;
        })
            .AddEntityFrameworkStores<ApplicationDbContext>();

    }
}