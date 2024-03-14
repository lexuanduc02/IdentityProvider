using Microsoft.AspNetCore.Mvc;

namespace IdentityProvider;

public class OauthController : Controller
{
    public IActionResult Login()
    {
        return View();
    }
}
