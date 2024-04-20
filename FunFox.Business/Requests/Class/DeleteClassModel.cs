using FunFox.Business.Requests.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Business.Requests.Class
{
    public class DeleteClassRequest : IRequest<DeleteClassResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteClassResponse : BaseResponse
    {

    }

}
