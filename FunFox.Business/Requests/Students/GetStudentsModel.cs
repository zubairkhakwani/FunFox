using FunFox.Business.Enums;
using FunFox.Business.Requests.Shared;
using MediatR;

namespace FunFox.Business.Requests.Students
{
    public class GetStudentsRequest : PageableRequest, IRequest<PageableResponse<GetStudentsResponse>>
    {
        public int? Id { get; set; }
        public ClassLevel? ClassLevel { get; set; }
    }

    public class GetStudentsResponse
    {
        public int Id { get; set;}
        public int UserId { get; set;}
        public string Name { get; set;}
        public string Email { get; set;}
        public ClassLevel Level { get; set;}
    }
}
