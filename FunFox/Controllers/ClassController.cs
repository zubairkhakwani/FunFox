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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int? studentId = identityUser.IsAuthenticated ? identityUser.StudentId : null;

            var getClassesRequest = new GetClassesRequest { PageNo = 1, PageSize = 50, StudentId = studentId };
            var response = await mediator.Send(getClassesRequest);

            return View(response);
        }

        [HttpPost("Enroll")]
        [Authorize(Roles = nameof(Roles.Student))]
        public async Task<IActionResult> Enroll([FromQuery] int courseId)
        {
            var enrollenmentRequest = new EnrollenmentRequest { CourseId = courseId, StudentId = identityUser.StudentId.Value };
            var response = await mediator.Send(enrollenmentRequest);

            return Redirect($"/?{(response.Success ? "msg" : "err")}={response.Message}");
        }

        [HttpPost("Delete/{classId}")]
        [Authorize(Roles = nameof(Roles.Admin))]
        public async Task<IActionResult> DeleteClass(int classId)
        {
            var response = await mediator.Send(new DeleteClassRequest { Id = classId });

            return Redirect($"/admin/classes?{(response.Success ? "msg" : "err")}={response.Message}");
        }
    }
}
