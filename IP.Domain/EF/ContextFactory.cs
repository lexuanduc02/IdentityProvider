using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IP.Domain;

public class ContextFactory : IDesignTimeDbContextFactory<IdentityProviderContext>
{
    public IdentityProviderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<IdentityProviderContext>();
        optionsBuilder.UseNpgsql("Server=192.168.1.32;Port=5432;Database=IdentityProvider;Username=postgres;Password=1");

        return new IdentityProviderContext(optionsBuilder.Options);
    }
}
