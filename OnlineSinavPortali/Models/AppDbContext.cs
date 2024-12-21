using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace OnlineSinavPortali.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)

        {

        }
        
        public DbSet<Exams> Exams { get; set; }

        public DbSet<ExamResults> ExamResults { get; set; }
        public DbSet<Questions> Questions { get; set; }
        public DbSet<StudentAnswers> StudentAnswers { get; set; }



    }
}