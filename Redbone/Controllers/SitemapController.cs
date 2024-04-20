using System.Text;
using Microsoft.AspNetCore.Mvc;
using Redbone.Services;
using X.Web.Sitemap;
using X.Web.Sitemap.Extensions;

namespace Redbone.Controllers;

[Route("/sitemap.xml")]
public class SitemapController : Controller
{
    private readonly SqlService _sql;
    private readonly IConfiguration _configuration;

    public SitemapController(SqlService sql, IConfiguration configuration)
    {
        _sql = sql;
        _configuration = configuration;
    }
    
    [HttpGet]
    public IActionResult Index()
    {
        var sitemap = new Sitemap();
        var posts = _sql.GetAllPosts();
        string[] nodes = _configuration.GetSection("SitemapNodes").Get<string[]>();
        
        string host = Request.Scheme + "://" + Request.Host;

        if (posts.Any())
        {
            foreach (var item in posts)
            {
                sitemap.Add(new Url
                {
                    Location = $"{host}/{item.Id}",
                    TimeStamp = item.Date,
                    ChangeFrequency = ChangeFrequency.Weekly,
                    Priority = 1.0
                });
            }
        }

        foreach (var item in nodes)
        {
            sitemap.Add(new Url
            {
                Location = $"{host}{item}",
                TimeStamp = DateTime.Now,
                ChangeFrequency = ChangeFrequency.Monthly,
                Priority = 0.8
            });
        }

        return Content(sitemap.ToXml(), "application/xml", Encoding.UTF8);
    }
}