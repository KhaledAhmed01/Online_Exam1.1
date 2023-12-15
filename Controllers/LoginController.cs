using Microsoft.AspNetCore.Mvc;
using Online_Exam.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
public class LoginController : Controller
{
    private readonly OnlineExammDbContext db;

    public LoginController(OnlineExammDbContext context)
    {
        db = context;
    }
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Signup()
    {
        return View(new Users());
    }

    [HttpPost] 
    public ActionResult Signup(Users user)
    {
        var isEmailExist = db.Users.Any(x => x.U_Email == user.U_Email);
        if (isEmailExist)
        {
            ModelState.AddModelError("U_Email", "Email is already exists");
            return View();
        }

        if (ModelState.IsValid)
        {
            if (user.U_Password == user.ConfirmPassword)
            {
                user.U_Password = HashPassword(user.U_Password);
                user.ConfirmPassword = HashPassword(user.ConfirmPassword);

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Signin", "Login");
            }
            else
            {
                ViewBag.Message = "Password and confirm password do not match.";
                return View(user);
            }
        }
        return View(user);
    }

    public IActionResult Signin()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Signin(Users model)
    {
        var user = db.Users.FirstOrDefault(x => x.U_Email == model.U_Email);

        if (user != null && VerifyPassword(model.U_Password, user.U_Password))
        {
            HttpContext.Session.SetString("U_Email", user.U_Email);

            // Set a flag to indicate that the user is authenticated
            ViewBag.IsAuthenticated = true;

            return RedirectToAction("Index", "Home");
        }
        else
        {
            if (user == null)
            {
                ModelState.AddModelError("U_Email", "Email not found");
            }
            else
            {
                ModelState.AddModelError("U_Password", "Invalid password");
            }

            ViewBag.IsAuthenticated = false;
        }

        return View(model);
    }



    public ActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Signin");
    }
    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
        //return "********";
    }

    private bool VerifyPassword(string inputPassword, string hashedPassword)
    {
        return HashPassword(inputPassword) == hashedPassword;
    }
}