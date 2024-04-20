using FunFox.Business.Enums;
using FunFox.Business.Requests.Class;
using FunFox.Business.Requests.Shared;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FunFox.Pages.Admin.Course
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ViewClassesModel : PageModel
    {
        private readonly IMediator mediator;
        public PageableResponse<GetClassesForAdminResponse> PageableResponse { get; set; }

        public ViewClassesModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task OnGet()
        {
            PageableResponse = await mediator.Send(new GetClassesForAdminRequest { });

        }
    }
}
