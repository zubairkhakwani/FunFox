using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Data.DbModels
{
    public class StudentClasses
    {
        public int Id { get; set; }

        [Required]
        public int ClassId { get; set; }

        [Required]
        public int StudentId { get; set; }

        public double TotalAmount { get; set; }
        public double FinalAmount { get; set; }
        public string TransactionId { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string PaymentGateway { get; set; } = null!;
        public bool IsSuccess { get; set; }
        public double Discount { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
