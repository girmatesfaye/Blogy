using Microsoft.AspNetCore.Mvc;
using Blogy_MVC.Repository;

namespace Blogy_MVC.Controllers
{
    public class AdminBlogPostController : Controller
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

    }
}