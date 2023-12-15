using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Models;
using System;
using System.Linq;

public class QuestionController : Controller
{
	private readonly OnlineExammDbContext _context;

	public QuestionController(OnlineExammDbContext context)
	{
		_context = context;
	}
	// GET: Questions/Create
	public IActionResult Create()
	{
		var Name = HttpContext.Session.GetString("U_Email");
		var examId = HttpContext.Session.GetInt32("Exam_id");
		if (string.IsNullOrEmpty(Name))
		{
			return RedirectToAction("Signin", "Login");
		}
		if (examId == null)
		{
			return RedirectToAction("Index", "Home");
		}
		Questions q = new Questions();
		return View();
	}
	// POST: Questions/Create
	[HttpPost]
	//[ValidateAntiForgeryToken]
	public IActionResult Create(Questions question)
	{
		//question.Exam_id = HttpContext.Session.GetInt32("Exam_id").Value;
		int examId = HttpContext.Session.GetInt32("Exam_id").Value;
		question.Exam_id = examId;
		_context.Add(question);
		_context.SaveChanges();
		HttpContext.Session.SetInt32("Question_id", question.Question_id);
		return RedirectToAction("Create", "Choices"); // Adjust the action and controller accordingly
													  //HttpContext.Session.SetInt32("Question_id", question.Question_id);

	}
	// GET: Questions/Edit/5
	//public IActionResult Edit(int? id)
	//{
	//    if (id == null)
	//    {
	//        return NotFound();
	//    }

	//    Questions question = _context.Questions.Find(id);

	//    if (question == null)
	//    {
	//        return NotFound();
	//    }

	//    return View(question);
	//}

	// POST: Questions/Edit/5
	//[HttpPost]
	//[ValidateAntiForgeryToken]
	//public IActionResult Edit(int id, Questions question)
	//{
	//    if (id != question.Question_id)
	//    {
	//        return NotFound();
	//    }

	//    if (ModelState.IsValid)
	//    {
	//        try
	//        {
	//            _context.Entry(question).State = EntityState.Modified;
	//            _context.SaveChanges();
	//        }
	//        catch (DbUpdateConcurrencyException)
	//        {
	//            if (!QuestionExists(question.Question_id))
	//            {
	//                return NotFound();
	//            }
	//            else
	//            {
	//                throw;
	//            }
	//        }

	//        // Redirect to wherever you need to go next
	//        return RedirectToAction("Index", "Home"); // Adjust the action and controller accordingly
	//    }

	//    return View(question);
	//}

	//// GET: Questions/Delete/5
	//public IActionResult Delete(int? id)
	//{
	//    if (id == null)
	//    {
	//        return NotFound();
	//    }

	//    Questions question = _context.Questions.Find(id);

	//    if (question == null)
	//    {
	//        return NotFound();
	//    }

	//    return View(question);
	//}

	//// POST: Questions/Delete/5
	//[HttpPost, ActionName("Delete")]
	//[ValidateAntiForgeryToken]
	//public IActionResult DeleteConfirmed(int id)
	//{
	//    Questions question = _context.Questions.Find(id);
	//    _context.Questions.Remove(question);
	//    _context.SaveChanges();

	//    // Redirect to wherever you need to go next
	//    return RedirectToAction("Index", "Home"); // Adjust the action and controller accordingly
	//}
	public IActionResult Finish()
	{
		HttpContext.Session.Remove("Question_id");
		return RedirectToAction("Create");
	}

}
