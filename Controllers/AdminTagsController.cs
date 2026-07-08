
using Blogy_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blogy_MVC.Data;

using Blogy_MVC.Models.Domain;

namespace Blogy_MVC.Controllers
{
   public class AdminTagsController : Controller
    {
       private readonly BlogyDbContext _blogyDbContext;
       public AdminTagsController(BlogyDbContext blogyDbContext)
        {
            _blogyDbContext = blogyDbContext;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

       //Mapping the AddTagRequest(The ViewModels ) that used form data binding to the Domain Model(the real one)
       [HttpPost]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
         var tag = new Tag
         {
             Name = addTagRequest.Name,
             DisplayName = addTagRequest.DisplayName
         };

        _blogyDbContext.Tags.Add(tag);
        _blogyDbContext.SaveChanges();

            return View("Add");

        }

        [HttpGet]
        public IActionResult List()
        {
         var tags = _blogyDbContext.Tags.ToList();
            return View(tags);

        }
    }
}