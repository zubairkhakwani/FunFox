using Azure.Core;
using FunFox.Business.CustomDataAnnotations;
using FunFox.Business.Requests.Class;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace FunFox.Pages.Admin.Course
{
    public class AddCourseModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IMediator mediator;

        public AddCourseModel(IWebHostEnvironment _webHostEnvironment, IMediator mediator)
        {
            webHostEnvironment = _webHostEnvironment;
            this.mediator = mediator;
        }

        //this property is here just so we can bind its fields with form
        public AddClassRequest Model { get; set; } = new();

        [BindProperty]
        [Required(ErrorMessage = "Select image to upload")]
        [AllowedExtensions(new string[] { ".jpg", ".jpeg", ".png", }, ErrorMessage = "Allowed file types are .jpg, .jpeg, .png")]
        public IFormFile Image { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost([Bind] AddClassRequest model) 
        { 
            if(!ModelState.IsValid)
            {
                return Page();
            }

            var uploadedFilePath = UploadImageToFolder();

            model.ImagePath = uploadedFilePath;

            var response = await mediator.Send(model);

            if(!response.Success)
            {
                ViewData["error"] = response.Message;
                return Page();
            }

            return Redirect("/admin/classes?msg=Class added successfully");
        }

        private string UploadImageToFolder()
        {
            var fileName = Image.FileName;

            var folderName = "ClassImages";

            string uploadsFolder = Path.Combine(webHostEnvironment.ContentRootPath, "wwwroot", folderName);
            var uniqueFileName = Guid.NewGuid().ToString() + fileName;

            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Image.CopyTo(fileStream);
            }

            return Path.Combine(folderName, uniqueFileName);
        }
    }
}
