using Octokit;
using LazyCache;
using Redbone.Models;
using Redbone.Services;
using Microsoft.AspNetCore.Mvc;
using Activity = System.Diagnostics.Activity;

namespace Redbone.Controllers;

public class HomeController(SqlService sql, IAppCache cache, IConfiguration configuration) : Controller
{
    [HttpGet]
    [Route("/")]
    public IActionResult Index()
    {
        string username = configuration.GetValue<string>("GithubAPI:UserName");
        string applicationName = configuration.GetValue<string>("GithubAPI:ApplicationName");

        var posts = sql.GetAllPosts().Reverse().Take(3);
        var client = new GitHubClient(new ProductHeaderValue(applicationName));

        List<Repository> RepositoryGetter()
        {
            var repositories = client.Repository.GetAllForUser(username).Result;
            
            return repositories
                .Where(x => !x.Fork && x.Name != username)
                .OrderBy(x => x.CreatedAt)
                .Reverse()
                .Take(3)
                .ToList();
        }

        var repositoryCache = cache.GetOrAdd("HomeController.Get", RepositoryGetter);
        
        return View(new HomeData { Posts = posts, Repositories = repositoryCache });
    }
    
    [HttpGet]
    [Route("/{id:int}")]
    public IActionResult Post(int id)
    {
        sql.IncreaseViewCountByPostId(id);

        var post = sql.GetPostById(id);
        
        if (post == null)
            return NotFound();
        
        return View(post);
    }
    
    [Route("/error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}