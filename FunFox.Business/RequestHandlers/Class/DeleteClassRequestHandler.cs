using FunFox.Business.Requests.Class;
using FunFox.Business.Services.Contracts;
using FunFox.Data;
using FunFox.Data.DbModels;
using MediatR;

namespace FunFox.Business.RequestHandlers.Class
{
    public class DeleteClassRequestHandler : IRequestHandler<DeleteClassRequest, DeleteClassResponse>
    {
        private readonly FunFoxDbContext dbContext;
        private readonly IIdentityUser identityUser;

        public DeleteClassRequestHandler(FunFoxDbContext dbContext, IIdentityUser identityUser)
        {
            this.dbContext = dbContext;
            this.identityUser = identityUser;
        }

        public async Task<DeleteClassResponse> Handle(DeleteClassRequest request, CancellationToken cancellationToken)
        {
            var classToEdit = await dbContext.Classes.FindAsync(request.Id);

            if (classToEdit == null)
            {
                //no need to entertain false requests.
                return new DeleteClassResponse { Success = true, Message = "Class deleted successfully" };
            }

            classToEdit.IsActive = false;

            dbContext.Classes.Update(classToEdit);
            await dbContext.SaveChangesAsync();

            return new DeleteClassResponse { Success = true, Message = "Class deleted successfully" };
        }
    }
}
