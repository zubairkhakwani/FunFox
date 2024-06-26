﻿using FunFox.Business.Enums;
using FunFox.Business.Requests.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Business.Requests.User
{
    public class RegisterRequest : IRequest<RegisterResponse>
    {
        [Required]
        public string Name { get; set; } = null!;

        [Required]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,})+)$", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        [Compare(nameof(Password), ErrorMessage = "Password and confirm password should be same")]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        public ClassLevel? Level { get; set; }
    }

    public class RegisterResponse : BaseResponse
    {
    }
}
