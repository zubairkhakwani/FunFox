using FunFox.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FunFox.Data
{
    public class FunFoxDbContext : DbContext
    {
        public FunFoxDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentClasses> StudentClasses { get; set; }
    }
}
