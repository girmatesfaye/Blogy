using Microsoft.AspNetCore.Mvc;
// using Blogy_MVC.Repository;

namespace Blogy_MVC.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        // private readonly IBlogPostRepository _blogPostRepository;
        // public AdminBlogPostsController(IBlogPostRepository blogPostRepository)
        // {
        //     _blogPostRepository = blogPostRepository;
        // }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
    }
}