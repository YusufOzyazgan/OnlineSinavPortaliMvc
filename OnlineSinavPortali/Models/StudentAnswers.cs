using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortali.Models
{
    public class StudentAnswers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentAnswerId { get; set; }

        [ForeignKey(nameof(User))]
        public string? StudentId { get; set; }

        [ForeignKey(nameof(Question))]
        public int QuestionId { get; set;}
        public int ExamId{ get; set;}  
        public string? AnswerText { get; set; }  


        public AppUser? User { get; set; }
        public Questions? Question { get; set; }
        public Exams? Exam { get; set; }



    }
}
