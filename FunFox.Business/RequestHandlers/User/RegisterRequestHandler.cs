using FunFox.Business.Enums;
using FunFox.Business.Requests.User;
using FunFox.Data;
using FunFox.Data.DbModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunFox.Business.RequestHandlers.User
{
    public class RegisterRequestHandler : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly FunFoxDbContext dbContext;

        public RegisterRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var userExists = (await dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == request.Email.ToLower())) != null;

            if(userExists)
            {
                return new RegisterResponse { Success = false, Message = "Email already exists, please register with different email or login to your existing acocunt." };
            }

            var user = new Data.DbModels.User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password,
                RoleId = (int)Roles.Student
            };

            var dbRequest = new Student
            {
                Level = (int)request.Level.Value,
                User = user
            };

            await dbContext.Students.AddAsync(dbRequest);
            await dbContext.SaveChangesAsync();

            return new RegisterResponse { Success = true, Message = "Registration Successful, login with your credentials" };
        }
    }
}
