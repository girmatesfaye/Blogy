
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
       [ActionName("Add")]
        public IActionResult Add(AddTagRequest addTagRequest)
        {
         var tag = new Tag
         {
             Name = addTagRequest.Name,
             DisplayName = addTagRequest.DisplayName
         };

        _blogyDbContext.Tags.Add(tag);
        _blogyDbContext.SaveChanges();

            return Redirect("List");
        }

        [HttpGet]
        [ActionName("List")]
        public IActionResult List()
        {
         var tags = _blogyDbContext.Tags.ToList();
            return View(tags);

        }

        [HttpGet]
        public IActionResult Edit(Guid id)
        {
            var tag = _blogyDbContext.Tags.FirstOrDefault(x => x.Id == id);
            if (tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    DisplayName = tag.DisplayName
                };
                return View(editTagRequest);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        [ActionName("Edit")]
        public IActionResult Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var existingTag = _blogyDbContext.Tags.Find(tag.Id);
            if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                // Save the changes
                _blogyDbContext.SaveChanges();

                //Show Success Message
                return RedirectToAction("Edit", new { id = editTagRequest.Id });
            }else
            {
            //Show Error Message
              return RedirectToAction("Edit", new { id = editTagRequest.Id });
           }  
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult Delete(EditTagRequest editTagRequest)
        {
            var tag = _blogyDbContext.Tags.Find(editTagRequest.Id);
            if (tag != null)
            {
                _blogyDbContext.Tags.Remove(tag);
                _blogyDbContext.SaveChanges();
            //show success message
            return RedirectToAction("List");
            }
            
            //show error message    
           return RedirectToAction("List");

        }
    }
}
