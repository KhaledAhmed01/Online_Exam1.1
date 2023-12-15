using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Exam.Models;
using System;
using System.Linq;
public class ChoicesController : Controller
{
	private readonly OnlineExammDbContext _context;

	public ChoicesController(OnlineExammDbContext context)
	{
		_context = context;
	}

	// Index action to display all choices
	public IActionResult Index()
	{
		int examId = HttpContext.Session.GetInt32("Exam_id").Value;
		int questionId = HttpContext.Session.GetInt32("Question_id").Value;

		var choices = _context.Choices
			.Where(c => c.Exam_id == examId && c.Question_id == questionId)
			.ToList();

		return View(choices);
	}

	// Create action to add a new choice
	public IActionResult Create()
	{
		var Name = HttpContext.Session.GetString("U_Email");
		var examId = HttpContext.Session.GetString("Exam_id");
		var questionId = HttpContext.Session.GetString("Question_id");
		if (string.IsNullOrEmpty(Name))
		{
			return RedirectToAction("Signin", "Login");
		}
		if (examId == null)
		{
			return RedirectToAction("Index", "Home");
		}
		if (questionId == null)
		{
			return RedirectToAction("Index", "Home");
		}
		Choices c = new Choices();
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public IActionResult Create(Choices choice)
	{
		int examId = HttpContext.Session.GetInt32("Exam_id").Value;
		int questionId = HttpContext.Session.GetInt32("Question_id").Value;
		choice.Exam_id = examId;
		choice.Question_id = questionId;
		_context.Choices.Add(choice);
		_context.SaveChanges();
		return RedirectToAction("Index");
	}

	// Edit action to update an existing choice
	//public IActionResult Edit(int id)
	//{
	//    var choice = _context.Choices.Find(id);

	//    if (choice == null)
	//    {
	//        return NotFound();
	//    }

	//    return View(choice);
	//}

	/*[HttpPost]
    [ValidateAntiForgeryToken]*/
	//public IActionResult Edit(int id, Choices choice)
	//{
	//    if (id != choice.Choice_id)
	//    {
	//        return NotFound();
	//    }

	//    if (ModelState.IsValid)
	//    {
	//        _context.Entry(choice).State = EntityState.Modified;
	//        _context.SaveChanges();
	//        return RedirectToAction(nameof(Index));
	//    }

	//    return View(choice);
	//}

	// Details action to view details of a choice
	//public IActionResult Details(int id)
	//{
	//    var choice = _context.Choices.Find(id);

	//    if (choice == null)
	//    {
	//        return NotFound();
	//    }

	//    return View(choice);
	//}

	//// Delete action to delete a choice
	//public IActionResult Delete(int id)
	//{
	//    var choice = _context.Choices.Find(id);

	//    if (choice == null)
	//    {
	//        return NotFound();
	//    }

	//    return View(choice);
	//}

	//[HttpPost, ActionName("Delete")]
	//[ValidateAntiForgeryToken]
	//public IActionResult DeleteConfirmed(int id)
	//{
	//    var choice = _context.Choices.Find(id);
	//    _context.Choices.Remove(choice);
	//    _context.SaveChanges();
	//    return RedirectToAction(nameof(Index));
	//}
}
