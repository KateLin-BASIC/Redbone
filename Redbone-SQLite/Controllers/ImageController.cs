using Microsoft.AspNetCore.Mvc;
using Redbone.Attributes;
using Redbone.Models;
using Redbone.Services;

namespace Redbone.Controllers;

[Admin]
public class ImageController(SqlService sql) : Controller
{
    [HttpGet]
    [Route("/image")]
    public IActionResult Index()
    {
        var images = sql.GetAllImageData();
        return View(images);
    }
    
    [HttpPost]
    [Route("/image")]
    [ValidateAntiForgeryToken]
    public IActionResult Index(IFormFile file)
    {
        string path = $"{AppDomain.CurrentDomain.BaseDirectory}/wwwroot/images/";
        string newFileName = file.FileName.Replace(" ", "_");
        
        if (ModelState.IsValid && file.Length > 0 && file.ContentType.StartsWith("image/") && !System.IO.File.Exists(path + newFileName))
        {
            Directory.CreateDirectory(path);
            
            var stream = System.IO.File.OpenWrite(path + newFileName);
            file.CopyTo(stream);
            stream.Close();
            
            ViewBag.Message = "파일이 성공적으로 업로드 되었습니다.";
            ViewBag.ImageUrl = $"https://{Request.Host}/images/{newFileName}";

            int id = sql.GetAvailableImageId();
                
            sql.CreateImageData(new Image
            {
                Id = id,
                Location = path + newFileName,
                Date = DateTimeOffset.UtcNow.AddHours(9).ToUnixTimeSeconds()
            });
            
            return View(sql.GetAllImageData());
        }
        
        ViewBag.ErrorMessage = "이미지 업로드에 실패하였습니다.";
        return View(sql.GetAllImageData());
    }
    
    [HttpGet]
    [Route("/image/delete/{imageId:int}")]
    public IActionResult Delete(int imageId, bool confirm)
    {
        if (!sql.IsImageDataExist(imageId))
            return NotFound();
        
        ViewBag.Id = imageId;

        if (confirm)
        {
            var imageData = sql.GetImageDataById(imageId);
            
            sql.DeleteImageData(imageId);
            System.IO.File.Delete(imageData.Location);
            
            return Redirect("~/image");
        }
        
        return View();
    }
}