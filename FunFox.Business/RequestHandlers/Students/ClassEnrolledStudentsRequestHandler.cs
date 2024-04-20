using FunFox.Business.Requests.Students;
using FunFox.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FunFox.Business.RequestHandlers.Students
{
    public class ClassEnrolledStudentsRequestHandler : IRequestHandler<ClassEnrolledStudentsRequest, List<ClassEnrolledStudentsResponse>>
    {
        private readonly FunFoxDbContext dbContext;

        public ClassEnrolledStudentsRequestHandler(FunFoxDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<ClassEnrolledStudentsResponse>> Handle(ClassEnrolledStudentsRequest request, CancellationToken cancellationToken)
        {
            var response = await dbContext.StudentClasses
                            .Include(sc => sc.Class)
                            .Include(sc => sc.Student)
                            .ThenInclude(s => s.User)
                            .Where(sc => sc.ClassId == request.ClassId)
                            .Select(sc => new ClassEnrolledStudentsResponse
                            {
                                StudentId = sc.Student.User.Id,
                                StudentName = sc.Student.User.Name,
                                StudentEmail = sc.Student.User.Email,
                                TotalAmount = sc.TotalAmount,
                                Discount = sc.Discount,
                                FinalAmount = sc.FinalAmount,
                                EnrolledAt = sc.PaymentDate.ToShortDateString(),
                                PaymentGateway = sc.PaymentGateway,
                                TransactionId = sc.TransactionId
                            })
                            .ToListAsync();


            return response;
        }
    }
}
