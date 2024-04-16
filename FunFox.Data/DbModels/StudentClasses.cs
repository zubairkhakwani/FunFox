using System.ComponentModel.DataAnnotations.Schema;

namespace FunFox.Data.DbModels
{
    public class StudentClasses
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Class))]
        public int? ClassId { get; set; }

        [ForeignKey(nameof(Student))]
        public int? StudentId { get; set; }

        public double TotalAmount { get; set; }
        public double FinalAmount { get; set; }
        public string TransactionId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PaymentGateway { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public double Discount { get; set; }
        public DateTime PaymentDate { get; set; }

        public Student? Student { get; set; }
        public Class? Class { get; set; }
    }
}
