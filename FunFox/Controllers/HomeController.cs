using FunFox.Business.Requests.Class;
using FunFox.Business.Services.Contracts;
using FunFox.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FunFox.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator mediator;
        private readonly IIdentityUser identityUser;

        public HomeController(ILogger<HomeController> logger, IMediator mediator, IIdentityUser identityUser)
        {
            _logger = logger;
            this.mediator = mediator;
            this.identityUser = identityUser;
        }

        public async Task<IActionResult> Index()
        {
            int? studentId = identityUser.IsAuthenticated ? identityUser.StudentId : null;

            var getClassesRequest = new GetClassesRequest { PageNo = 1, PageSize = 50, StudentId = studentId };
            var response = await mediator.Send(getClassesRequest);

            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
