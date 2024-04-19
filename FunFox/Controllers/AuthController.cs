using FunFox.Business.Enums;
using FunFox.Business.Requests.User;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FunFox.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator mediator;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthController(IMediator mediator, IHttpContextAccessor httpContextAccessor)
        {
            this.mediator = mediator;
            this.httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequest request)
        {
            if(!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await mediator.Send(request);
            ViewBag.SuccessMessage = "Registration Success";

            return View();
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var response = await mediator.Send(request);

            if(!response.Success)
            {
                ModelState.AddModelError("login-error", response.Message);
                return View();
            }

            var claims = GetClaims(response);

            //httpcontext.signinasync

            var claimsIdentity = new ClaimsIdentity(
            claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties();
            authProperties.ExpiresUtc = DateTime.UtcNow.AddDays(5);
            authProperties.IsPersistent = true;

            await httpContextAccessor.HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

            var redirectURL = response.Role == Roles.Admin.ToString() ? "/admin/classes" : "/";

            return Redirect(redirectURL);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await httpContextAccessor.HttpContext.SignOutAsync();

            return Redirect("/");
        }

        private IEnumerable<Claim> GetClaims(LoginResponse model)
        {
            List<Claim> claims = new Claim[] {
                new Claim("Id", model.Id.ToString()),
                new Claim(ClaimTypes.Name, model.Name),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Role, model.Role),
                new Claim("Level", model.Level.ToString())
            }.ToList();

            if(model.StudentId != null)
            {
                claims.Add(new Claim("StudentId", model.StudentId.ToString()));
            }

            return claims;
        }
    }
}
