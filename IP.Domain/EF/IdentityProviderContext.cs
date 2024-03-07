using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IP.Domain;

public class IdentityProviderContext : IdentityDbContext<User, Role, Guid>
{
    public IdentityProviderContext(DbContextOptions<IdentityProviderContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("Users").HasKey(x => x.Id);
        builder.Entity<Role>().ToTable("Roles").HasKey(x => x.Id);

        builder.Entity<IdentityUserRole<Guid>>(entity =>
        {
            entity.ToTable("UserRoles");
            entity.HasKey(x => new { x.RoleId, x.UserId });
        });

        builder.Entity<IdentityUserClaim<Guid>>(entity =>
        {
            entity.ToTable("UserClaims");
        });

        builder.Entity<IdentityUserLogin<Guid>>(entity =>
        {
            entity.ToTable("UserLogins");
            entity.HasKey(x => x.UserId);
        });

        builder.Entity<IdentityRoleClaim<Guid>>(entity =>
        {
            entity.ToTable("RoleClaims");
        });

        builder.Entity<IdentityUserToken<Guid>>(entity =>
        {
            entity.ToTable("UserTokens");
            entity.HasKey(x => x.UserId);
        });

        builder.Entity<RoleClient>().HasKey(x => new { x.RoleId, x.ClientId });
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RoleClient> RoleClients { get; set; }
}
