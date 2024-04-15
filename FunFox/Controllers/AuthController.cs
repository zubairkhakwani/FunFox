using FunFox.Business.Requests.User;
using FunFox.Data.DbModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FunFox.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
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

            return Redirect("/");
        }
    }
}
