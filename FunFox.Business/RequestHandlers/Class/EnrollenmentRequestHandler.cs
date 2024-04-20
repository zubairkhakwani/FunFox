using FunFox.Business.Requests.Class;
using FunFox.Data;
using FunFox.Data.DbModels;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FunFox.Business.RequestHandlers.Class
{
    public class EnrollenmentRequestHandler : IRequestHandler<EnrollenmentRequest, EnrollenmentResponse>
    {
        private readonly FunFoxDbContext dbContext;

        public EnrollenmentRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<EnrollenmentResponse> Handle(EnrollenmentRequest request, CancellationToken cancellationToken)
        {
            int? maxClassSize = (await dbContext.Classes.FirstOrDefaultAsync(c => c.Id == request.CourseId))?.ClassSize;

            var enrolledStudents = await dbContext.StudentClasses
                .Where(sc => sc.ClassId == request.CourseId)
                .ToListAsync();

            if(enrolledStudents.Count() >= maxClassSize)
            {
                return new EnrollenmentResponse { Success = false, Message = "Class size is full" };
            }

            var dbRequest = new StudentClasses
            {
                ClassId = request.CourseId,
                StudentId = request.StudentId,
                PaymentGateway = "None",
                TotalAmount = 1000,
                FinalAmount = 990,
                TransactionId = "asdasdash12i3h213n",
                Status = "Success",
                IsSuccess = true,
                Discount = 10,
                PaymentDate = DateTime.UtcNow,
            };

            dbContext.StudentClasses.Add(dbRequest);
            await dbContext.SaveChangesAsync();

            return new EnrollenmentResponse { Success = true, Message = "Enrollenment success" };
        }
    }
}
