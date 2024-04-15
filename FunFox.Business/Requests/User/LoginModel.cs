using FunFox.Business.Requests.Shared;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace FunFox.Business.Requests.User
{
    public class LoginRequest : IRequest<LoginResponse>
    {
        [Required]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;
    }

    public class LoginResponse : BaseResponse
    {

    }
}
