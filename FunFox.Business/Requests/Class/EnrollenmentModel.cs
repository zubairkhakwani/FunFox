using FunFox.Business.Requests.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Business.Requests.Class
{
    public class EnrollenmentRequest : IRequest<EnrollenmentResponse>
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
    }

    public class EnrollenmentResponse : BaseResponse
    {
    }
}
