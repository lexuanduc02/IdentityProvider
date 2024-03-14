using IP.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IP.Domain;

public static class ModelBuilderExtension
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasData(new Role
        {
            Id = new Guid("C17D487E-646A-47E2-9EE4-B319155E326E"),
            Name = "admin",
            NormalizedName = "admin"
        });

        var hasher = new PasswordHasher<User>();
        modelBuilder.Entity<User>().HasData(new User
        {
            Id = new Guid("D7D6AE65-8029-46C5-A006-F89D6D04FA8C"),
            UserName = "admin",
            Email = "lms@hou.edu.vn",
            NormalizedEmail = "lms@hou.edu.vn",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "lxduc11@111"),
            SecurityStamp = string.Empty,
            FirstName = "Đại học",
            LastName = "Mở Hà Nội",
            Gender = GenderEnum.Male,
            Dob = new DateTime(1993, 11, 03, 0, 0, 0),
            PhoneNumber = "024 3868 2321",
        });

        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = new Guid("C17D487E-646A-47E2-9EE4-B319155E326E"),
            UserId = new Guid("D7D6AE65-8029-46C5-A006-F89D6D04FA8C")
        });
    }
}
