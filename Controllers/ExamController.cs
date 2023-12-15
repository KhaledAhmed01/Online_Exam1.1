using Microsoft.AspNetCore.Mvc;
using Online_Exam.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
public class ExamController : Controller
{

	private readonly OnlineExammDbContext db;

	public ExamController(OnlineExammDbContext context)
	{
        db = context;
	}

	public IActionResult Create()
	{
		var Name = HttpContext.Session.GetString("U_Email");
		if (string.IsNullOrEmpty(Name))
		{
			return RedirectToAction("Signin", "Login");
		}
		Exam exam = new Exam();
		return View(exam);
	}

	[HttpPost]
	public IActionResult Create(Exam exam)
	{

		string adminEmail = HttpContext.Session.GetString("U_Email");
		exam.Adminstration_Email = adminEmail;
		db.Exams.Add(exam);
		db.SaveChanges();
		HttpContext.Session.SetInt32("Exam_id", exam.Exam_id);
		return RedirectToAction("Create", "Question");
	}

	public IActionResult Index()
	{
        var Name = HttpContext.Session.GetString("U_Email");
        if (string.IsNullOrEmpty(Name))
        {
            return RedirectToAction("Signin", "Login");
        }
        string adminEmail = HttpContext.Session.GetString("U_Email");
		var exams = db.Exams
			.Where(e => e.Adminstration_Email == adminEmail)
			.ToList();

		return View(exams);
	}
    public IActionResult Details(int id)
    {
        var exam = db.Exams.FirstOrDefault(e => e.Exam_id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    // Update Exam (GET)
    public IActionResult Edit(int id)
    {
        var exam = db.Exams.FirstOrDefault(e => e.Exam_id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    [HttpPost]
    public IActionResult Edit(int id, Exam updatedExam)
    {
        if (id != updatedExam.Exam_id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            var existingExam = db.Exams.Find(id);
            existingExam.Exam_title = updatedExam.Exam_title;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        return View(updatedExam);
    }

    public IActionResult Delete(int id)
    {
        var exam = db.Exams.FirstOrDefault(e => e.Exam_id == id);

        if (exam == null)
        {
            return NotFound();
        }

        return View(exam);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        var exam = db.Exams.Find(id);

        if (exam == null)
        {
            return NotFound();
        }

        db.Exams.Remove(exam);
        db.SaveChanges();

        return RedirectToAction("Index");
    }
    [HttpPost]
    public IActionResult SaveExamCode(int examCode)
    {
        HttpContext.Session.SetInt32("ExamCode", examCode);
		return NoContent();
    }
    public IActionResult Finish()
	{
		HttpContext.Session.Remove("Exam_id");
		return RedirectToAction("Index", "Home");
	}
}