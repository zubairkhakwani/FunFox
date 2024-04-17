using FunFox.Business.Requests.Class;
using FunFox.Business.Services.Contracts;
using FunFox.Data;
using FunFox.Data.DbModels;
using MediatR;

namespace FunFox.Business.RequestHandlers.Class
{
    public class AddClassRequestHandler : IRequestHandler<AddClassRequest, AddClassResponse>
    {
        private readonly FunFoxDbContext dbContext;
        private readonly IIdentityUser identityUser;

        public AddClassRequestHandler(FunFoxDbContext dbContext, IIdentityUser identityUser)
        {
            this.dbContext = dbContext;
            this.identityUser = identityUser;
        }

        public async Task<AddClassResponse> Handle(AddClassRequest request, CancellationToken cancellationToken)
        {
            var dbRequest = new FunFox.Data.DbModels.Class()
            {
                Title = request.Title,
                DetailHTML = request.DetailHTML,
                ClassSize = request.ClassSize.Value,
                Level = (int)request.Level.Value,
                Image = request.ImagePath,
                ClassFrom = request.ClassFrom.Value,
                ClassTo = request.ClassTo.Value,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                CreatedById = identityUser.Id,
            };

            await dbContext.Classes.AddAsync(dbRequest);
            await dbContext.SaveChangesAsync();

            return new AddClassResponse { Success = true, Message = "Class added successfully" };
        }
    }
}
