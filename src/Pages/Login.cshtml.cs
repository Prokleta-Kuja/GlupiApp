using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class Login : PageModel
{
    private readonly IHttpContextAccessor _httpContextAccssor;

    [BindProperty]
    public string UserName { get; set; }
    [BindProperty]
    public string Password { get; set; }


    public Login(IHttpContextAccessor httpContextAccssor)
    {
        _httpContextAccssor = httpContextAccssor;
    }

    public async Task<IActionResult> OnGet()
    {
        if (HttpContext.User != null && HttpContext.User.Identity != null && HttpContext.User.Identity.IsAuthenticated)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/");
        }
        else
            return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        await LoginUser();
        return Redirect("/");
    }
    public async Task<bool> LoginUser()
    {
        var claims = new List<Claim>{
                new Claim(ClaimTypes.Upn, "kifla"),
                new Claim(ClaimTypes.Email, "kifla@kifle.com"),
                new Claim(ClaimTypes.Role, "Admin")
            };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        return true;
    }
}