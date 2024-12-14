using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Redbone.Models;

namespace Redbone.Controllers;

[Route("/login")]
public class LoginController(IConfiguration configuration) : Controller
{
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
        string name = configuration.GetValue<string>("Administrator:Name"), 
            hash = Environment.GetEnvironmentVariable("REDBONE_ADMIN_HASH");

        if (ModelState.IsValid && user.Name == name && HashVerification(user.Password, hash))
        {
            HttpContext.Session.SetString("_isAuthed", "true");
            return Redirect("~/");
        }

        ViewBag.Message = "로그인에 실패하였습니다.";
        return View();

        bool HashVerification(string passwordToVerification, string hashToVerification)
        {
            //Base64로 디코딩되었던 해시를 변환함.
            byte[] hashBytes = Convert.FromBase64String(hashToVerification);
            
            //16 Byte Salt를 추출
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            
            //추출한 Salt를 바탕으로 비밀번호를 생성
            var pbkdf2 = new Rfc2898DeriveBytes(passwordToVerification, salt, 100000, HashAlgorithmName.SHA512);
            byte[] generatedHash = pbkdf2.GetBytes(20);
            
            //생성된 해시가 저장된 해시와 같은지 확인
            return generatedHash.SequenceEqual(hashBytes.Skip(16));
        }
    }
}