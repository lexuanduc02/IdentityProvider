using IP.Domain;
using IP.Model;
using Microsoft.AspNetCore.Identity;

namespace IP.Services;

public class OauthService : IOauthServices
{
    private readonly UserManager<User> _userManager;

    public OauthService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<BaseResponseModel<User>> Login(LoginRequestModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return new BaseResponseModel<User>
            {
                Success = false,
                Message = "Invalid username"
            };
        }

        var result = await _userManager.CheckPasswordAsync(user, model.Password);

        if (result)
        {
            return new BaseResponseModel<User>
            {
                Success = true,
                Data = user
            };
        }

        return new BaseResponseModel<User>
        {
            Success = false,
            Message = "Invalid password"
        };
    }
}
