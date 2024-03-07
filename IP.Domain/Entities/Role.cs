using Microsoft.AspNetCore.Identity;

namespace IP.Domain;

public class Role : IdentityRole<Guid>
{
    public string? Description { get; set; }
}
