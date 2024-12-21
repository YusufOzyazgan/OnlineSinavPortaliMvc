using Microsoft.AspNetCore.SignalR;
using System.Drawing;

namespace OnlineSinavPortali.ViewModels
{
    public class ExamResultModel
    {
        public int ExamResultID {  get; set; } 

        public int ExamID {  get; set; } 
        public string? StudentName { get; set; }
        public DateTime ExamDate { get; set; }  
        public int Score { get; set; }

        public string? StudentId { get; set; }

        public string? ClassName { get; set; }   
        public string? LessonName { get; set; }  
        
    }
}
