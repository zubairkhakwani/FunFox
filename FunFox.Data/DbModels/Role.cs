using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FunFox.Data.DbModels
{
    [Index(nameof(Name), IsUnique = true)]
    public class Role
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
