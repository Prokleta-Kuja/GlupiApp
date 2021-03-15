using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GlupiApp.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class Login : PageModel
{
    private readonly AppDbContext _db;
    private readonly IHttpContextAccessor _httpContextAccssor;

    [BindProperty]
    public string UserName { get; set; }
    [BindProperty]
    public string Password { get; set; }
    public string Message { get; set; }


    public Login(IHttpContextAccessor httpContextAccssor, AppDbContext db)
    {
        _httpContextAccssor = httpContextAccssor;
        _db = db;
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
        Message = string.Empty;
        var user = _db.Users.SingleOrDefault(u => u.UserName == UserName);
        if (user == null)
        {
            Message = "Ne postoji korisnik";
            return Page();
        }
        else if (user.Password != Password)
        {
            Message = "Netočna lozinka";
            return Page();
        }

        var claims = new List<Claim>
        {
                new Claim(ClaimTypes.Upn, user.UserName),
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
        };
        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = true
        };
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

        return Redirect("/");
    }
}