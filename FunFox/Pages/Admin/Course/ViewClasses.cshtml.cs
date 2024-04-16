using FunFox.Business.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FunFox.Pages.Admin.Course
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class ViewClassesModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
