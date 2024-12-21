using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using NuGet.Versioning;
using OnlineSinavPortali.Models;
using OnlineSinavPortali.ViewModels;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;

namespace OnlineSinavPortali.Controllers
{
    public class ExamController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notify;
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ExamController(AppDbContext context, INotyfService notify, UserManager<AppUser> userManager, ILogger<HomeController> logger)
        {
            _context = context;
            _notify = notify;
            _userManager = userManager;
            _logger = logger;
        }



        public IActionResult ExamListAjax()
        {
            var UserID = _userManager.GetUserId(User);
            var examModel = _context.Exams.Where(s => s.TeacherId == UserID)
                    .Select(x => new
                    ExamModel
                    {
                        UniversityDepartment = x.UniversityDepartment,
                        ExamID = x.ExamId,
                        CourseName = x.CourseName ?? "",
                        ExamType = x.ExamType,
                        ExamDate = x.ExamDate,
                    }).ToList();

            return Json(examModel);
        }



        public IActionResult DeleteQuestion(int id)
        {
            var questionToDelete = _context.Questions.Find(id);

            var examId = questionToDelete.ExamId;

            _context.Questions.Remove(questionToDelete);
            _context.SaveChanges();
            _notify.Warning("Soru silindi");
            // Başarılı bir şekilde silindiğini belirten bir mesaj veya başka bir işlem yapabilirsiniz.
            return RedirectToAction("Questions", "Exam", new { id = examId });

        }







        public IActionResult CreateExam()
        {

            return View();
        }

        [HttpPost]
        public IActionResult CreateExam(string courseId, string examType, string examDateTime, string UniversityDepartment)
        {

            try
            {
                var userId = _userManager.GetUserId(User);
                DateTime parsedDateTime = DateTime.Parse(examDateTime);

                Exams newExam = new()
                {
                    UniversityDepartment = UniversityDepartment,
                    TeacherId = userId,
                    CourseName = courseId,
                    ExamDate = parsedDateTime,
                    ExamType = examType
                };
                _context.Exams.Add(newExam);

                int affectedRows = _context.SaveChanges();

                return Ok(new { message = "Exam successfully created." });
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return BadRequest(new { message = "Error creating exam.", error = ex.Message });
            }









        }
        public IActionResult MyExams()
        {
            return View();
        }



        [HttpPost]
        public JsonResult GetQuestionsByExamID([FromBody] int examID)
        {
            try
            {
                var questions = _context.Questions
                    .Where(q => q.ExamId == examID)
                    .Select(q => new QuestionModel
                    {
                        QuestionID = q.QuestionId,
                        QuestionText = q.QuestionText,
                        QuestionAnswersText = q.QuestionAnswer,
                    })
                    .ToList();

                return Json(questions);
            }
            catch (Exception ex)
            {
                _logger.LogError("Sorular getirilirken hata oluştu: " + ex.Message);
                return Json(null);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Questions(int id)
        {
            TempData["examID"] = id;
            ViewBag.ExamID = id;
            var Exam = _context.Exams
            .FirstOrDefault(q => q.ExamId == id);
            var examType = Exam.ExamType;

            _logger.LogInformation("exam ID =====" + id);
            var questionsModel = await _context.Questions
            .Where(a => a.ExamId == id)
            .Select(x => new QuestionModel()
            {
                ExamType = examType,
                optionAText = x.optionAText,
                optionBText = x.optionBText,
                optionDText = x.optionDText,
                optionCText = x.optionCText,
                ExamID = x.ExamId,
                QuestionID = x.QuestionId,
                QuestionText = x.QuestionText,
                QuestionAnswersText = x.QuestionAnswer
            })
            .ToListAsync();
            _logger.LogInformation("exam Count =====" + questionsModel.Count);

            return View(questionsModel);


        }



        public IActionResult EditQuestion(int id)
        {


            var questionModel = _context.Questions
             .Where(a => a.QuestionId == id)
             .Select(x => new QuestionModel()
             {
                 optionAText = x.optionAText,
                 optionBText = x.optionBText,
                 optionDText = x.optionDText,
                 optionCText = x.optionCText,
                 ExamID = x.ExamId,
                 QuestionID = x.QuestionId,
                 QuestionText = x.QuestionText,
                 QuestionAnswersText = x.QuestionAnswer
             })
            .FirstOrDefault();

            return View(questionModel);
        }
        [HttpPost]
        public IActionResult EditQuestion(QuestionModel model)
        {

            if (model.QuestionAnswersText == null)
            {
                _notify.Error("Doğru cevabı işaretlemeniz gerekiyor.");
                return View(model);
            }

            var existingQuestion = _context.Questions
            .FirstOrDefault(q => q.QuestionId == model.QuestionID);
            if (existingQuestion != null)
            {
                existingQuestion.QuestionText = model.QuestionText;
                existingQuestion.QuestionAnswer = model.QuestionAnswersText;
                existingQuestion.optionAText = model.optionAText;
                existingQuestion.optionBText = model.optionBText;
                existingQuestion.optionCText = model.optionCText;
                existingQuestion.optionDText = model.optionDText;

                try
                {
                    _context.SaveChanges();
                    _notify.Success("Soru Güncellendi");
                    return RedirectToAction("Questions", "Exam", new { id = model.ExamID });

                }
                catch (Exception ex)
                {

                    _notify.Error("Soru Kaydedilirken bir hata oluştu. " + ex.Message);
                    return RedirectToAction("Questions", "Exam", new { id = model.ExamID });
                }

            }
            _notify.Error("Soru Kaydedilirken bir hata oluştu");
            return RedirectToAction("Questions", "Exam", new { id = model.ExamID });

        }

        public IActionResult CreateQuestion(int id)
        {
            ViewBag.ExamId = id;
            return View();
        }


        [HttpPost]
        public IActionResult CreateQuestion(QuestionModel model)
        {


            var Exam = _context.Exams
            .FirstOrDefault(q => q.ExamId == model.ExamID);

            //_logger.LogInformation($"soru id = {model.ExamID}");
            if (ModelState.IsValid)
            {

                var newQuestionModel = new Questions()
                {
                    ExamType = model.ExamType,
                    optionAText = model.optionAText,
                    optionBText = model.optionBText,
                    optionCText = model.optionCText,
                    optionDText = model.optionDText,
                    ExamId = model.ExamID,
                    QuestionText = model.QuestionText,
                    QuestionAnswer = model.QuestionAnswersText,
                };
                try
                {
                    _context.Questions.Add(newQuestionModel);
                    _context.SaveChanges();

                    _notify.Success("Soru Eklendi", 4);
                    return RedirectToAction("Questions", "Exam", new { id = model.ExamID });

                }
                catch (Exception ex)
                {

                    _notify.Error("Soru eklenirken hata oluştu. " + ex.Message);
                    return RedirectToAction("Questions", "Exam", new { id = model.ExamID });

                }





            }
            _notify.Error("Formda Hata Var");

            return View(model);
        }

        public IActionResult EditExam(int id)
        {
            var examModel = _context.Exams
             .Where(a => a.ExamId == id)
             .Select(x => new ExamModel()
             {
                 CourseName = x.CourseName ?? "",
                 UniversityDepartment = x.UniversityDepartment ?? "",
                 ExamType = x.ExamType,
                 ExamDate = x.ExamDate,
                 ExamID = x.ExamId,
             })
            .FirstOrDefault();

            return View(examModel);


        }
        [HttpPost]
        public IActionResult EditExam(ExamModel model)
        {


            DateTime examDateTime = model.ExamDate1.Date.Add(model.ExamTime.TimeOfDay);
            var existingQuestion = _context.Exams
            .FirstOrDefault(q => q.ExamId == model.ExamID);

            existingQuestion.CourseName = model.CourseName;
            existingQuestion.UniversityDepartment = model.UniversityDepartment;
            existingQuestion.ExamDate = examDateTime;

            _context.SaveChanges();
            _notify.Success("Soru Güncellendi");

            return RedirectToAction("MyExams");

        }



        public IActionResult ExamDelete(int id)
        {

            var exam = _context.Exams
            .FirstOrDefault(q => q.ExamId == id);

            TempData["examID"] = id;
            var examModel = new ExamModel()
            {
                CourseName = exam.CourseName ?? "",
                UniversityDepartment = exam.UniversityDepartment ?? "",
                ExamDate = exam.ExamDate,
                ExamID = id,
            };
            return View(examModel);
        }

        [HttpPost]
        public IActionResult ExamDelete()
        {
            int id = (int)TempData["examID"];
            TempData["examID"] = id;
            var exam = _context.Exams
            .FirstOrDefault(q => q.ExamId == id);
            _context.Exams.Remove(exam);
            _context.SaveChanges();
            _notify.Success("Sınav Silindi");
            return RedirectToAction("MyExams");
        }


        // STUDENT KISMI

        public IActionResult StudentMyExams()
        {
            var userId = _userManager.GetUserId(User);
            var user = _context.Users
            .FirstOrDefault(q => q.Id == userId);


            var examModel = _context.Exams
             .Where(a => a.UniversityDepartment == user.UniversityDepartment)
             .Select(x => new ExamModel()
             {
                 CourseName = x.CourseName ?? "",
                 UniversityDepartment = x.UniversityDepartment ?? "",
                 ExamDate = x.ExamDate,
                 ExamID = x.ExamId,
             })
            .ToList();

            return View(examModel);


        }
        public IActionResult EnterExam(int id)
        {
            var exam = _context.Exams
            .FirstOrDefault(q => q.ExamId == id);
            var now = DateTime.Now;
            var examDate = exam.ExamDate;
            var sure = now - examDate;
            if (sure.Days == 0 && sure.Hours == 0 && (sure.Minutes > 10 || sure.Minutes < 10))
            {
                var studentAnswerModel = new StudentAnswersModel()
                {
                    Questions = _context.Questions
             .Where(a => a.ExamId == id)
             .Select(x => new QuestionModel()
             {
                 optionAText = x.optionAText,
                 optionBText = x.optionBText,
                 optionCText = x.optionCText,
                 optionDText = x.optionDText,
                 QuestionText = x.QuestionText,
                 QuestionAnswersText = x.QuestionAnswer,
                 QuestionID = x.QuestionId
             }).ToList(),
                    ExamId = id
                };
                //_logger.LogInformation($"Geçen süre: {sure.Days} gün, {sure.Hours} saat, {sure.Minutes} dakika");
                return View(studentAnswerModel);
            }
            else
            {
                var zamanModel = new ZamanModel
                {
                    Sure = sure,
                };
                return RedirectToAction("ExamTimeOut", zamanModel);
            }



        }
        [HttpPost]
        public async Task<IActionResult> EnterExam(StudentAnswersModel model)
        {
            var examQuestions = await _context.Questions
               .Where(i => i.ExamId == model.ExamId)
               .Select(x => new QuestionModel()
               {
                   QuestionID = x.QuestionId,
                   QuestionAnswersText = x.QuestionAnswer

               }).ToListAsync();

            var trueAnswers = 0;
            if (model.StudentAnswers != null)
            {
                foreach (var i in model.StudentAnswers)
                {
                    foreach (var x in examQuestions)
                    {

                        if (i.Key == x.QuestionID && i.Value == x.QuestionAnswersText)
                        {
                            trueAnswers++;
                        }
                    }

                }

                int questionCount = examQuestions.Count;
                if (questionCount == 0)
                {
                    _notify.Error("Sunucu taraflı bir hata oluştu.");
                    return RedirectToAction("Index", "Home");

                }

                decimal percentageCalculation = ((100m / questionCount) * trueAnswers);
                int score = (int)Math.Round(percentageCalculation);
                var UserId = _userManager.GetUserId(User);

                foreach (var i in examQuestions)
                {
                    if (model.StudentAnswers.ContainsKey(i.QuestionID))
                    {
                        var answerStudent = model.StudentAnswers[i.QuestionID];


                        var UserAnswers = new StudentAnswers()
                        {
                            ExamId = model.ExamId,
                            QuestionId = i.QuestionID,
                            StudentId = UserId,
                            AnswerText = answerStudent
                        };

                        await _context.StudentAnswers.AddAsync(UserAnswers);
                    }
                    else
                    {
                        var UserAnswers = new StudentAnswers()
                        {
                            ExamId = model.ExamId,
                            QuestionId = i.QuestionID,
                            StudentId = UserId,
                            AnswerText = null
                        };

                        await _context.StudentAnswers.AddAsync(UserAnswers);
                    }
                }



                // Exam Result Part

                await _context.ExamResults.AddAsync(new ExamResults
                {
                    ExamId = model.ExamId,
                    UserId = UserId,
                    Score = score,
                });
                try
                {
                    await _context.SaveChangesAsync();
                    _notify.Success($"Sınavınız başarıyla kayıt edildi. Puanınız {score} ");
                }
                catch (Exception)
                {
                    _notify.Error("Sınavınız kayıt olurken bir hata oluştu.");
                    return View(model);
                }




            }



            return RedirectToAction("Index", "Home");
        }






        public IActionResult ExamTimeOut(ZamanModel model)
        {
            return View(model);
        }




        public async Task<IActionResult> MyExamResult()
        {
            var userID = _userManager.GetUserId(User);

            //class , lessonNAme
            var ExamResults = await _context.ExamResults.Where(x => x.UserId == userID).Include(b => b.Exam).Select(i => new ExamResultModel()
            {
                ExamResultID = i.ResultId,
                Score = i.Score,
                ExamID = i.ExamId,
                ClassName = i.Exam!.UniversityDepartment ?? "",
                LessonName = i.Exam.CourseName,
                ExamDate = i.Exam.ExamDate


            }).ToListAsync();

            return View(ExamResults);
        }

        public async Task<IActionResult> ExamResultsByTeacher()
        {
            var UserId = _userManager.GetUserId(User);

            var ExamIdList = await _context.Exams.Where(x => x.TeacherId == UserId).Select(y => y.ExamId).ToListAsync();
            var examResults = await _context.ExamResults.Where(x => ExamIdList.Contains(x.ExamId)).Include(y => y.User).Include(a => a.Exam).Select(i => new ExamResultModel()
            {
                ExamResultID = i.ResultId,
                Score = i.Score,
                ExamID = i.ExamId,
                StudentName = i.User!.FullName ?? "",
                ClassName = i.Exam!.UniversityDepartment,
                LessonName = i.Exam.CourseName,
                ExamDate = i.Exam.ExamDate,
                StudentId = i.User.Id


            }).ToListAsync();

            return View(examResults);
        }

        // https://localhost:7107/Exam/StudentAnswers/4/bad4961b-1900-49bc-b8f3-e451941c0797


        //https://localhost:7107/ExamController/StudentAnswers?examId=5&userId=bad4961b-1900-49bc-b8f3-e451941c0797
        // https://localhost:7107/Exam/StudentAnswers?examId=5&userId=bad4961b-1900-49bc-b8f3-e451941c0797



        public async Task<IActionResult> StudentAnswers(int examId, string userId)
        {
            var userAnswers = await _context.StudentAnswers.Where(x => x.StudentId == userId && x.ExamId == examId).Include(y => y.Question).Select(a => new StudentQuestionsModel()
            {
                AnswerText = a.AnswerText,
                QuestionAnswer = a.Question.QuestionAnswer,
                optionAText = a.Question.optionAText,
                optionBText = a.Question.optionBText,
                optionCText = a.Question.optionCText,
                optionDText = a.Question.optionDText,
                QuestionText = a.Question.QuestionText

            }).ToListAsync();

            return View(userAnswers);
        }




    }
}





