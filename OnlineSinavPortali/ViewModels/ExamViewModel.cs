namespace OnlineSinavPortali.ViewModels
{
    public class ExamViewModel
    {
        public List<StudentAnswersModel> Questions { get; set; }
        public int? ExamID { get; set; }
        public string? TeacherID { get; set; }
        public string? ExamType { get; set; }
    }
}
