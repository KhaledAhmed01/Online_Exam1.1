using Microsoft.AspNetCore.Mvc;
using Online_Exam.Models;

namespace Online_Exam.Controllers
{
    public class PrintController : Controller
    {
        private readonly OnlineExammDbContext db;

        public PrintController(OnlineExammDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var x = HttpContext.Session.GetString("U_Email");
            if (string.IsNullOrEmpty(x))
            {
                return RedirectToAction("Signin", "Login");
            }
            var Name = HttpContext.Session.GetInt32("ExamCode").Value;
            ViewBag.mess = Name;
            List<Exam> ListExam = db.Exams.ToList();
            List<Questions> ListQuestions = db.Questions.ToList();
            List<Choices> ListChoices = db.Choices.ToList();
            PrintViewModel obViewModel = new PrintViewModel
            {
                ExamViewModel = ListExam,
                QuestionsViewModel = ListQuestions,
                ChoicesViewModel = ListChoices
            };

            return View(obViewModel);
        }
    }
}
