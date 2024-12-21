using OnlineSinavPortali.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineSinavPortali.ViewModels
{
    public class StudentQuestionsModel
    {
        //public int StudentAnswerId { get; set; }

     
        //public string? StudentId { get; set; }

        
        //public int QuestionId { get; set; }
        //public int ExamId { get; set; }

        public string? QuestionText { get; set; }
        public string? AnswerText { get; set; }
        public string? QuestionAnswer { get; set; }
        public string? optionAText { get; set; }
        public string? optionBText { get; set; }
        public string? optionCText { get; set; }
        public string? optionDText { get; set; }




    }
}
