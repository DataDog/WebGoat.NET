using Microsoft.AspNetCore.Mvc;
using WebGoat.NET.Data;

namespace WebGoat.NET.Controllers;

[Route("Blog")]
public class BlogController(BlogRepository blogRepository) : Controller
{
    public IActionResult Index()
    {
        var posts = blogRepository.GetPosts();
        return View(posts);
    }
    
    [Route("{id}")]
    public IActionResult Index(int id)
    {
        var post = blogRepository.GetPostById(id);
        if (post is null)
        {
            return RedirectToAction("Index");
        }
        
        return View("Post", new { Post = post, Comments = blogRepository.GetCommentsForPost(id, null) });
    }
    
    [HttpPost]
    public IActionResult Comment(int postId, string comment)
    {
        if (User.Identity is null || !User.Identity.IsAuthenticated)
        {
            return RedirectToAction("Login", "Account");
        }
        
        if (postId <= 0)
        {
            return RedirectToAction("Index");
        }
        
        if (string.IsNullOrEmpty(User.Identity.Name) || string.IsNullOrEmpty(comment))
        {
            return RedirectToAction("Index", new { id = postId });
        }

        var authorName = User.Identity.Name;
        blogRepository.CreateComment(postId, authorName, comment);
        return RedirectToAction("Index", new { id = postId });
    }
}