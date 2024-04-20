using MediatR;

namespace FunFox.Business.Requests.Students
{
    public class ClassEnrolledStudentsRequest : IRequest<List<ClassEnrolledStudentsResponse>>
    {
        public int ClassId { get; set; }
    }

    public class ClassEnrolledStudentsResponse
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string StudentEmail { get; set;}
        public string EnrolledAt { get; set; }
        public double TotalAmount { get; set; }
        public double Discount { get; set; }
        public double FinalAmount { get; set; }
        public string TransactionId { get; set; } = null!;
        public string PaymentGateway { get; set; } = null!;
    }
}
