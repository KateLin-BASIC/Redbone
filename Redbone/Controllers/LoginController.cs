using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Redbone.Models;

namespace Redbone.Controllers;

[Route("/login")]
public class LoginController : Controller
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        if (HttpContext.Session.GetString("_isAuthed") == "true")
            return Redirect("~/panel");
        
        return View();
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Index(LoginModel user)
    {
        bool HashVerification(string passwordToVerification, string hashToVerification)
        {
            byte[] hashBytes = Convert.FromBase64String(hashToVerification);
            
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            
            var pbkdf2 = new Rfc2898DeriveBytes(passwordToVerification, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
            
            return hash.SequenceEqual(hashBytes.Skip(16));
        }

        string name = _configuration.GetValue<string>("Administrator:Name"), 
            hash = Environment.GetEnvironmentVariable("REDBONE_ADMIN_HASH");

        if (ModelState.IsValid && user.Name == name && HashVerification(user.Password, hash))
        {
            HttpContext.Session.SetString("_isAuthed", "true");
            return Redirect("~/");
        }

        ViewBag.Message = "로그인에 실패하였습니다.";
        return View();
    }
}