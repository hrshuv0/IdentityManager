using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WEB.Extensions;

public static class ApplicationServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        services.ConfigureApplicationCookie(opt =>
        {
            opt.AccessDeniedPath = new PathString("/Home/AccessDenied");
            opt.LoginPath = new PathString("/Identity/Account/Login");
        });

    }
}