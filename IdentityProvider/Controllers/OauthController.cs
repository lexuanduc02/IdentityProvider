using System.Security.Claims;
using IP.Model;
using IP.Services;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider;

public class OauthController : Controller
{
    private readonly IOauthServices _oauthServices;

    public OauthController(IOauthServices oauthServices)
    {
        _oauthServices = oauthServices;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequestModel model)
    {
        var result = await _oauthServices.Login(model);

        if (result.Success)
        {
            if (Url.IsLocalUrl(model.ReturnUrl))
            {
                var claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name, result.Data.FirstName + " " + result.Data.LastName),
                    new Claim(ClaimTypes.Email, result.Data.Email),
                    new Claim(ClaimTypes.Sid, result.Data.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, "pwd");
                var user = new ClaimsPrincipal(identity);

                // await HttpContext.SignInAsync(user);

                return Redirect(model.ReturnUrl);
            }
            else
                return View();
        }

        ModelState.AddModelError("", result.Message);

        return View();
    }
}
