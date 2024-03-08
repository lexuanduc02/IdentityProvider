using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;
using IP.Common;
using IP.Domain;
using Microsoft.AspNetCore.Identity;
using Serilog.Events;

namespace IdentityProvider;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly ILogger<ResourceOwnerPasswordValidator> _logger;
    private const string ClassName = nameof(ResourceOwnerPasswordValidator);

    public ResourceOwnerPasswordValidator(UserManager<User> userManager,
        SignInManager<User> signInManager,
        ILogger<ResourceOwnerPasswordValidator> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var username = context.UserName;

        try
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user != null && user.IsActive == true)
            {
                _logger.LogInformation("Password Accepted".GeneratedLog(ClassName, LogEventLevel.Information));

                //set the result
                context.Result = new GrantValidationResult(
                    subject: user.Id.ToString(),
                    authenticationMethod: "password",
                    claims: new List<Claim> {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Email, user.Email)
                    }
                );

                return;
            }
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");

            _logger.LogError("User does not exist.".GeneratedLog(ClassName, LogEventLevel.Error));
            return;
        }
        catch (Exception ex)
        {
            _logger.LogError($"Invalid username or password: {ex}".GeneratedLog(ClassName, LogEventLevel.Error));
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
        }

    }
}