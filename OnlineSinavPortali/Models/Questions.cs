using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortali.Models
{
    public class Questions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QuestionId {  get; set; }
        public int ExamId { get; set; }
        public string? ExamType { get; set; }    
        public string? QuestionText { get; set; }
        public string? QuestionAnswer { get; set; }
        public string? optionAText { get; set; }
        public string? optionBText { get; set; }
        public string? optionCText { get; set; }
        public string? optionDText { get; set; }
        public Exams? Exam { get; set; } 
        public StudentAnswers? StudentAnswer { get; set; }
    }
}
