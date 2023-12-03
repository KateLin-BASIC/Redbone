using Microsoft.AspNetCore.Mvc;
using Redbone.Attributes;
using Redbone.Models;
using Redbone.Services;

namespace Redbone.Controllers;

[Admin]
public class ImageController : Controller
{
    private readonly SqlService _sql;

    public ImageController(SqlService sql)
    {
        _sql = sql;
    }
    
    [HttpGet]
    [Route("/image")]
    public IActionResult Index()
    {
        var images = _sql.GetAllImageData();
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
            
            _sql.CreateImageData(new Image
            {
                Location = path + newFileName,
                Date = DateTime.Now.AddHours(9)
            });
            
            return View(_sql.GetAllImageData());
        }
        
        ViewBag.ErrorMessage = "이미지 업로드에 실패하였습니다.";
        return View(_sql.GetAllImageData());
    }
    
    [HttpGet]
    [Route("/image/delete/{imageId:int}")]
    public IActionResult Delete(int imageId, bool confirm)
    {
        if (!_sql.IsImageDataExist(imageId))
            return NotFound();
        
        ViewBag.Id = imageId;

        if (confirm)
        {
            var imageData = _sql.GetImageDataById(imageId);
            
            _sql.DeleteImageData(imageId);
            System.IO.File.Delete(imageData.Location);
            
            return Redirect("~/image");
        }
        
        return View();
    }
}