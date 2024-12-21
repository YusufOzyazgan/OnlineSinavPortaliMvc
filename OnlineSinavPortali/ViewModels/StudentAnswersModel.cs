namespace OnlineSinavPortali.ViewModels
{
    public class StudentAnswersModel
    {
        public List<QuestionModel>? Questions { get; set; }
        public int ExamId { get; set; }
       public Dictionary<int, string>? StudentAnswers { get; set; }
    }
}
