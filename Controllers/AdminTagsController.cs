
using Blogy_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blogy_MVC.Data;
using Microsoft.EntityFrameworkCore;
using Blogy_MVC.Models.Domain;
using Blogy_MVC.Repository;

namespace Blogy_MVC.Controllers
{
   public class AdminTagsController : Controller
    {
       private readonly ITagRepository _tagRepository;
       public AdminTagsController(ITagRepository tagRepository)
        {
            _tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

       //Mapping the AddTagRequest(The ViewModels ) that used form data binding to the Domain Model(the real one)
       [HttpPost]
       [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
         var tag = new Tag
         {
             Name = addTagRequest.Name,
             DisplayName = addTagRequest.DisplayName
         };
            await _tagRepository.AddAsync(tag);
            return Redirect("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            //use db context to get all tags from database
         var tags = await _tagRepository.GetAllAsync();

         return View(tags);

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await _tagRepository.GetAsync(id);
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
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Name = editTagRequest.Name,
                DisplayName = editTagRequest.DisplayName
            };
            var updatedTag = await _tagRepository.UpdateAsync(tag);
            if (updatedTag != null)
            {
                updatedTag.Name = tag.Name;
                updatedTag.DisplayName = tag.DisplayName;
                //Show Success Message
                 return RedirectToAction("List");
            }else
            {
            //Show Error Message
             return NotFound();
           }  
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await _tagRepository.DeleteAsync(editTagRequest.Id);
            if (deletedTag != null)
            {
                //show success message
                return RedirectToAction("List");
            }
               //show error message 
                return NotFound(); 
        }
    }
}
