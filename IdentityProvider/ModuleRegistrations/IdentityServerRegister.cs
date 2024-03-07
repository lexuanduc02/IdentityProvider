using IP.Domain;
using Microsoft.EntityFrameworkCore;

namespace IdentityProvider;

public class IdentityServerRegister
{
    public static void Initialize(IServiceCollection services, string connectionString)
    {
        var migrationsAssembly = "IP.Domain";
        services.AddIdentityServer()
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
