using Redbone.Services;
using Microsoft.AspNetCore.Mvc;

namespace Redbone.Controllers;

[Route("/recent/{page:int?}")]
public class RecentController : Controller
{
    private readonly SqlService _sql;

    public RecentController(SqlService sql)
    {
        _sql = sql;
    }
    
    [HttpGet]
    public IActionResult Index(int page = 1)
    {
        const int size = 5;
        
        var posts = _sql.GetAllPosts()?.Reverse().ToList();
        var data = posts.Skip((page - 1) * size).Take(size);

        if (!posts.Any() && page != 1)
            return Redirect("~/recent");
        
        if (!posts.Any())
            return View();

        ViewBag.Page = page;
        ViewBag.MaxPage = posts.Count / size + (posts.Count % size == 0 ? 0 : 1);
        
        ViewBag.PreviousPage = ViewBag.Page > 1 ? $"/recent/{ViewBag.Page - 1}" : null;
        ViewBag.NextPage = ViewBag.Page < ViewBag.MaxPage ? $"/recent/{ViewBag.Page + 1}" : null;

        if (page <= 0 ||  page > ViewBag.MaxPage)
            return Redirect("~/recent");

        return View(data);
    }
}