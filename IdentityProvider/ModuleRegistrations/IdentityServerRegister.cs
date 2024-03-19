using IP.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider;

public class IdentityServerRegister
{
    public static void Initialize(IServiceCollection services, string connectionString)
    {
        var migrationsAssembly = typeof(IdentityProviderContext).Assembly.GetName().Name;
        services.AddIdentityServer(options =>
                {
                    options.UserInteraction.LoginUrl = "/oauth/login";
                    options.UserInteraction.LogoutUrl = "/oauth/logout";
                    options.Authentication.CookieLifetime = TimeSpan.FromHours(1);
                    options.Authentication.CookieSlidingExpiration = false;
                })
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
                })
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>();
    }
}
