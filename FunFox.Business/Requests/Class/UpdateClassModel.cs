using FunFox.Business.Enums;
using FunFox.Business.Requests.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FunFox.Business.Requests.Class
{
    public class UpdateClassRequest : IRequest<UpdateClassResponse>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter title")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Please enter class detail")]
        public string DetailHTML { get; set; }

        [Required(ErrorMessage = "Please select class start time")]
        public TimeOnly? ClassFrom { get; set; }

        [Required(ErrorMessage = "Please select class end time")]
        public TimeOnly? ClassTo { get; set; }

        [Required(ErrorMessage = "Please enter max class size")]
        public int? ClassSize { get; set; }

        public ClassLevel? Level { get; set; }
    }

    public class UpdateClassResponse : BaseResponse
    {

    }

}
