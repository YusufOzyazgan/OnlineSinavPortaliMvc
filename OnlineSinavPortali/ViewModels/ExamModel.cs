using System.ComponentModel.DataAnnotations;

namespace OnlineSinavPortali.ViewModels
{
    public class ExamModel
    {

        [Required(ErrorMessage = "Bu alan boş geçilemez!!")]
        public string CourseName { get; set; }

        [Required(ErrorMessage = "Bu alan boş geçilemez!!")]
        public string UniversityDepartment {  get; set; }

        public int ExamID { get; set; }

        
        public string? ExamType { get; set; }

        //[Required(ErrorMessage = "Bu alan boş geçilemez!!")]
        //public DateOnly ExamDate1 { get; set; }
            

        //[Required(ErrorMessage = "Bu alan boş geçilemez!!")]
        //public TimeOnly ExamTime { get; set; }    

        public DateTime ExamDate { get; set; }


        [DataType(DataType.Date)]
        public DateTime ExamDate1 { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime ExamTime { get; set; }

    }
}
