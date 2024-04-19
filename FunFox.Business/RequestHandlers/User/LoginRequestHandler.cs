using FunFox.Business.Enums;
using FunFox.Business.Requests.User;
using FunFox.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Business.RequestHandlers.User
{
    internal class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly FunFoxDbContext dbContext;

        public LoginRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = await dbContext
                .Users
                .Include(u => u.Student)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower() && u.Password == request.Password);

            if(user == null)
            {
                return new LoginResponse { Success = false, Message = "Invalid credentials" };
            }

            return new LoginResponse { 
                Success = true,
                Message = "Login success",
                Id = user.Id,
                StudentId = user.Student?.Id,
                Email = user.Email,
                Level = user.Role.Name == Roles.Student.ToString() ? (ClassLevel)user.Student.Level : null,
                Name = user.Name,
                Role = user.Role.Name
            };
        }
    }
}
