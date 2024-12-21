using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortali.Models
{
    public class Exams 
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExamId { get; set; }

        public string? UniversityDepartment {  get; set; }

        [ForeignKey(nameof(User))]
        public string? TeacherId { get; set; }  
        public string? CourseName { get; set; }
        public string? ExamType {  get; set; }
        public DateTime ExamDate { get; set; }  
        public List<ExamResults>? ExamResult { get; set; }
        public AppUser? User { get; set; }  

        public List<StudentAnswers>? StudentAnswers { get; set; }
        

    }
}
