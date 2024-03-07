using Microsoft.AspNetCore.Identity;

namespace IP.Domain;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public bool IsActive { get; set; }

    public string? UserCode { get; set; }

    public string? Title { get; set; }

    public string? AvatarLink { get; set; }

    public int Gender { get; set; }

    public DateTime Dob { get; set; }
}
