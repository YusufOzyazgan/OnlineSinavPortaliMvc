using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortali.Models
{
    public class ExamResults
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultId { get; set; }
        public int ExamId { get; set; }
        public string? UserId { get; set; }
        public int Score { get; set; }
        public Exams? Exam { get; set; }
        public AppUser? User { get; set; }



    }
}

