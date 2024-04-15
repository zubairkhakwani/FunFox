using FunFox.Business.Enums;
using FunFox.Business.Requests.User;
using FunFox.Data;
using FunFox.Data.DbModels;
using MediatR;

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

            return new RegisterResponse { Success = true, Message = "Student added successfully" };
        }
    }
}
