using System.Security.Claims;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IP.Domain;
using Microsoft.AspNetCore.Identity;

namespace IdentityProvider;

public class ProfileService : IProfileService
{
    private readonly UserManager<User> _userManager;

    public ProfileService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var userId = context.Subject.Identity.GetSubjectId();

        var user = await _userManager.FindByIdAsync(userId);

        var claims = new List<Claim>
            {
                new Claim("name", $"{user.FirstName} {user.LastName}" ?? "Guest"),
                new Claim("email", user.Email ?? ""),
                new Claim("avatar", user.ImageLink ?? ""),
            };
        context.IssuedClaims.AddRange(claims);

        var roles = await _userManager.GetRolesAsync(user);
        if (roles != null)
        {
            foreach (var role in roles)
            {
                context.IssuedClaims.Add(new Claim(JwtClaimTypes.Role, role));
            }
        }
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        string sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);
        context.IsActive = user != null;

    }
}