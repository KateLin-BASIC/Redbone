using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Redbone.Attributes;
using Redbone.Models;
using Redbone.Services;

namespace Redbone.Controllers;

[Admin]
public class PanelController : Controller
{
    private readonly SqlService _sql;

    public PanelController(SqlService sql)
    {
        _sql = sql;
    }
    
    [HttpGet]
    [Route("/panel")]
    public IActionResult Index(bool logout, string lastUrl)
    {
        if (logout)
        {
            HttpContext.Session.SetString("_isAuthed", "false");
            return Redirect(string.IsNullOrWhiteSpace(lastUrl) || lastUrl.StartsWith("/panel") || lastUrl.StartsWith("/image") ? "~/" : lastUrl);
        }
        
        return View();
    }
    
    [HttpPost]
    [Route("/panel")]
    [ValidateAntiForgeryToken]
    public IActionResult Index(string password)
    {
        string GenerateHash(string passwordToHash)
        {
            //16 Bytes 크기의 Salt를 생성하고 대입함.
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            //pbkdf2를 사용해 20 Bytes 크기의 Hash를 생성하고 대입함.
            var pbkdf2 = new Rfc2898DeriveBytes(passwordToHash, salt, 100000);
            byte[] hash = pbkdf2.GetBytes(20);
    
            //Salt와 Hash를 합쳐 대입함. (Salt 16 Bytes + Hash 20 Bytes = 36 Bytes)
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
    
            //합쳐서 만든 Hash를 Base64로 변환해 String으로 반환함.
            return Convert.ToBase64String(hashBytes);
        }

        ViewBag.Message = GenerateHash(password);
        return View();
    }
    
    [HttpGet]
    [Route("/panel/write")]
    public IActionResult Write()
    {
        return View("Write");
    }
    
    [HttpPost]
    [Route("/panel/write")]
    [ValidateAntiForgeryToken]
    public IActionResult Write(WriteModel data)
    {
        if (ModelState.IsValid)
        {
            int id = _sql.CreatePost(new Post
            {
                Title = data.Title,
                Content = data.Content,
                Date = DateTime.Now.AddHours(9)
            });

            return Redirect($"~/{id}");
        }

        ViewBag.Message = "알맞은 내용을 입력해 주십시오.";
        return View("Write");
    }
    
    [HttpGet]
    [Route("/panel/edit/{postId:int}")]
    public IActionResult Edit(int postId)
    {
        var post = _sql.GetPostById(postId);
        
        if (post == null)
            return Redirect("~/");
        
        return View("Edit", post);
    }
    
    [HttpPost]
    [Route("/panel/edit/{postId:int}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int postId, WriteModel data)
    {
        var post = _sql.GetPostById(postId);
        
        if (post == null)
            return Redirect("~/");
        
        if (ModelState.IsValid)
        {
            _sql.UpdatePost(postId, new Post
            {
                Id = postId,
                Title = data.Title,
                Content = data.Content,
                Date = post.Date,
                Views = post.Views
            });
            
            return Redirect($"~/{postId}");
        }

        ViewBag.Message = "알맞은 내용을 입력해 주십시오.";
        return View("Edit");
    }

    [HttpGet]
    [Route("/panel/delete/{postId:int}")]
    public IActionResult Delete(int postId, bool confirm)
    {
        if (!_sql.IsPostExist(postId))
            return NotFound();
        
        ViewBag.Id = postId;

        if (confirm)
        {
            _sql.DeletePost(postId);
            return Redirect("~/");
        }
        
        return View("Delete");
    }
}