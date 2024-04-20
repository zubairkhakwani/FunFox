using FunFox.Business.Enums;
using FunFox.Business.Requests.Shared;
using MediatR;

namespace FunFox.Business.Requests.Class
{
    public class GetClassesRequest : PageableRequest, IRequest<PageableResponse<GetClassesResponse>>
    {
        public int? StudentId { get; set; }
        public int? Id { get; set; }
    }

    public class GetClassesResponse
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string DetailHTML { get; set; } = null!;
        public TimeOnly ClassFrom { get; set; }
        public TimeOnly ClassTo { get; set; }
        public int ClassSize { get; set; }
        public ClassLevel Level { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool AlreadyEnrolled { get; set; }

        public int StudentCount { get; set; }
    }
}
