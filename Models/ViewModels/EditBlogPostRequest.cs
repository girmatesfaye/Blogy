using Blogy_MVC.Models.Domain;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Blogy_MVC.Models.ViewModels
{
    public class EditBlogPostRequest
    {
        public Guid Id { get; set; }
        public string Heading { get; set; }
        public string PageTitle { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public string FeatureImageUrl { get; set; }
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        public string Author { get; set; }
        public bool Visible { get; set; }

        // Display tags
        public IEnumerable<SelectListItem> Tags { get; set;}
        // Collection of selected tag IDs
        public string[] SelectedTags { get; set; } = Array.Empty<string>();
    }
}