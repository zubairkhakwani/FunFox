using FunFox.Business.Enums;
using FunFox.Business.Requests.Class;
using FunFox.Business.Services.Contracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FunFox.Controllers
{
    [Route("[controller]")]
    public class ClassController : Controller
    {
        private readonly IIdentityUser identityUser;
        private readonly IMediator mediator;

        public ClassController(IIdentityUser identityUser, IMediator mediator)
        {
            this.identityUser = identityUser;
            this.mediator = mediator;
        }

        [HttpPost("Enroll")]
        [Authorize(Roles = nameof(Roles.Student))]
        public async Task<IActionResult> Index([FromQuery] int courseId)
        {
            var enrollenmentRequest = new EnrollenmentRequest { CourseId = courseId, StudentId = identityUser.StudentId.Value};
            var response = await mediator.Send(enrollenmentRequest);

            return Redirect($"/?{(response.Success ? "msg" : "err")}={response.Message}");
        }
    }
}
