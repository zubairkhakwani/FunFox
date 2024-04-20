using FunFox.Business.CustomDataAnnotations;
using FunFox.Business.Requests.Class;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace FunFox.Pages.Admin.Course
{
    public class EditClassModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMediator mediator;

        public EditClassModel(IWebHostEnvironment _webHostEnvironment, IMediator mediator)
        {
            webHostEnvironment = _webHostEnvironment;
            this.mediator = mediator;
        }

        //this property is here just so we can bind its fields with form
        public UpdateClassRequest Model { get; set; } = new();

        public async Task OnGet(int id)
        {
            var response = await mediator.Send(new GetClassesRequest { Id = id });

            var classToEdit = response.Records.FirstOrDefault();

            if (classToEdit != null)
            {
                Model.Id = id;
                Model.ClassFrom = classToEdit.ClassFrom;
                Model.ClassTo = classToEdit.ClassTo;
                Model.DetailHTML = classToEdit.DetailHTML;
                Model.ClassSize = classToEdit.ClassSize;
                Model.Level = classToEdit.Level;
                Model.Title = classToEdit.Title;
            }
        }

        public async Task<IActionResult> OnPost([Bind] UpdateClassRequest model)
        {
            Model = model;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var response = await mediator.Send(model);

            if (!response.Success)
            {
                ViewData["error"] = response.Message;
                return Page();
            }

            return Redirect("/admin/classes?msg=Class updated successfully");
        }
    }
}
