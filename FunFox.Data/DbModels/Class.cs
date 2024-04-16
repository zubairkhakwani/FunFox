using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunFox.Data.DbModels
{
    public class Class
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Image { get; set; } = null!;

        [Required]
        public string DetailHTML { get; set; } = null!;

        [Required]
        public TimeOnly ClassFrom { get; set; }

        [Required]
        public TimeOnly ClassTo { get; set;}

        [Required]
        public int ClassSize { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(CreatedBy))]
        [Required]
        public int CreatedById { get; set; }
        public User CreatedBy { get; set; }

        public List<StudentClasses> StudentClasses { get; set; }
    }
}
