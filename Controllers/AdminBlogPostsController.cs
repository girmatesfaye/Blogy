using Blogy_MVC.Models.Domain;
using Blogy_MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Blogy_MVC.Repository;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace Blogy_MVC.Controllers
{
    public class AdminBlogPostsController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        public AdminBlogPostsController(ITagRepository tagRepository, IBlogPostRepository blogPostRepository
        )
        {
            _tagRepository = tagRepository;
            _blogPostRepository = blogPostRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            // Get Task From Repository
            var tags = await _tagRepository.GetAllAsync();

            var model = new AddBlogPostRequest
            {
                Tags = tags.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList()
            };
            return View(model);
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(AddBlogPostRequest addBlogPostRequest)
        {
            // Map View Model to Domain Model
            var blogPost = new BlogPost
            {
                Heading = addBlogPostRequest.Heading,
                PageTitle = addBlogPostRequest.PageTitle,
                Content = addBlogPostRequest.Content,
                ShortDescription = addBlogPostRequest.ShortDescription,
                FeatureImageUrl = addBlogPostRequest.FeatureImageUrl,
                UrlHandle = addBlogPostRequest.UrlHandle,
                PublishedDate = addBlogPostRequest.PublishedDate,
                Author = addBlogPostRequest.Author,
                Visible = addBlogPostRequest.Visible
            };
         
         // Map Tags from Selected Tags
         var SelectedTags = new List<Tag>();
         foreach (var selectedTagId in addBlogPostRequest.SelectedTags)
         {
            var selectedTagAsGuid = Guid.Parse(selectedTagId);
            var existingTag = await _tagRepository.GetAsync(selectedTagAsGuid);

            if(existingTag != null)
            {
               SelectedTags.Add(existingTag);  
            };
         }

         // Mapping Tags Back to the Domain Model
         blogPost.Tags = SelectedTags;
         await _blogPostRepository.AddAsync(blogPost);
            return RedirectToAction("List");
        }
        

       [HttpGet]
       public async Task<IActionResult> List(){
        // Call the BlogPostRepository to ask the DB and give all the list of Blog POST
       var allBlogPost = await _blogPostRepository.GetAllAsync();
        return View(allBlogPost);
       }

       [HttpGet]
       public async Task<IActionResult> Edit(Guid id)
        {
           var blogPost = await _blogPostRepository.GetAsync(id);
            if(blogPost != null)
            {
                var editBlogPostRequest = new EditBlogPostRequest
                {
                    Heading = blogPost.Heading,
                    
                    // PageTitle = blogPost.PageTitle,
                    // Content = blogPost.Content,
                    // ShortDescription = blogPost.ShortDescription,
                    // FeatureImageUrl = blogPost.FeatureImageUrl,
                    // UrlHandle = blogPost.UrlHandle,
                    // PublishedDate = blogPost.PublishedDate,
                    // Author = blogPost.Author,
                    // Visible = blogPost.Visible,
                };
                return View(editBlogPostRequest);
            }
            return RedirectToAction("List");
        }
    }
}