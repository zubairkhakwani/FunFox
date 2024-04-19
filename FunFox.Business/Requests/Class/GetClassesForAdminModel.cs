using FunFox.Business.Requests.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FunFox.Business.Enums;

namespace FunFox.Business.Requests.Class
{
    public class GetClassesForAdminRequest : PageableRequest, IRequest<PageableResponse<GetClassesForAdminResponse>>
    {

    }

    public class GetClassesForAdminResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string DetailHTML { get; set; } = null!;
        public TimeOnly ClassFrom { get; set; }
        public TimeOnly ClassTo { get; set; }
        public int ClassSize { get; set; }
        public ClassLevel Level { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public int StudentCount { get; set; }
    }
}
