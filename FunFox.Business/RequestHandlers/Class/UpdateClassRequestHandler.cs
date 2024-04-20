using FunFox.Business.Requests.Class;
using FunFox.Business.Services.Contracts;
using FunFox.Data;
using FunFox.Data.DbModels;
using MediatR;

namespace FunFox.Business.RequestHandlers.Class
{
    public class UpdateClassRequestHandler : IRequestHandler<UpdateClassRequest, UpdateClassResponse>
    {
        private readonly FunFoxDbContext dbContext;
        private readonly IIdentityUser identityUser;

        public UpdateClassRequestHandler(FunFoxDbContext dbContext, IIdentityUser identityUser)
        {
            this.dbContext = dbContext;
            this.identityUser = identityUser;
        }

        public async Task<UpdateClassResponse> Handle(UpdateClassRequest request, CancellationToken cancellationToken)
        {
            var classToEdit = await dbContext.Classes.FindAsync(request.Id);

            if (classToEdit == null)
            {
                //no need to entertain false requests.
                return new UpdateClassResponse { Success = true, Message = "Class updated successfully" };
            }

            classToEdit.Title = request.Title;
            classToEdit.DetailHTML = request.DetailHTML;
            classToEdit.ClassSize = request.ClassSize.Value;
            classToEdit.ClassFrom = request.ClassFrom.Value;
            classToEdit.ClassTo = request.ClassTo.Value;

            dbContext.Classes.Update(classToEdit);
            await dbContext.SaveChangesAsync();

            return new UpdateClassResponse { Success = true, Message = "Class updated successfully" };
        }
    }
}
