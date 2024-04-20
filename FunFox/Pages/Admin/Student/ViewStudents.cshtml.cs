using FunFox.Business.Enums;
using FunFox.Business.Requests.Class;
using FunFox.Business.Requests.Shared;
using FunFox.Business.Requests.Students;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FunFox.Pages.Admin.Student
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ViewStudentsModel : PageModel
    {
        private readonly IMediator mediator;
        public PageableResponse<GetStudentsResponse> PageableResponse { get; set; }

        [FromQuery(Name = "msg")]
        public string Message { get; set; }


        public ViewStudentsModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnGet()
        {
            PageableResponse = await mediator.Send(new GetStudentsRequest { });

        }
    }
}
