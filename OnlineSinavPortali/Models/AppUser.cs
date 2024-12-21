using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace OnlineSinavPortali.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }

        public string? PhotoUrl { get; set; }

        public string? StudentNumber { get; set; }

        public string? UniversityDepartment { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Exams>? Exams { get; set; }
        public List<ExamResults>? ExamResults { get; set; }
        public List<StudentAnswers>? StudentAnswers { get; set; }





    }
}