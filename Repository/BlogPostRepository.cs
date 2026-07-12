
using Blogy_MVC.Data;
using Blogy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogy_MVC.Repository
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly BlogyDbContext _blogyDbContext;
        public BlogPostRepository(BlogyDbContext blogDbContext)
        {
            _blogyDbContext = blogDbContext;
        }

        public async Task<BlogPost> AddAsync(BlogPost blogPost)
        {
            await _blogyDbContext.BlogPosts.AddAsync(blogPost);
            await _blogyDbContext.SaveChangesAsync();
            return blogPost;
        }

        public Task<BlogPost?> DeleteAsync(Guid id)
        {
            _blogyDbContext.BlogPosts.Remove(new BlogPost { Id = id });
            _blogyDbContext.SaveChangesAsync();
            return Task.FromResult<BlogPost?>(null);
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            // Accept the request from AdminBlogPostController
          return await _blogyDbContext.BlogPosts.Include(x => x.Tags).ToListAsync();
        }

        public async Task<BlogPost?> GetAsync(Guid id)
        {
          return await _blogyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {
            // Ask the DB to give the post to Edit and return back to the controller
           var existingBlog = await _blogyDbContext.BlogPosts.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Id == blogPost.Id);
           if(existingBlog != null)
            {
                existingBlog.Heading = blogPost.Heading;
                existingBlog.PageTitle = blogPost.PageTitle;
                existingBlog.PublishedDate = blogPost.PublishedDate;
                existingBlog.ShortDescription = blogPost.ShortDescription;
                existingBlog.Author = blogPost.Author;
                existingBlog.Content = blogPost.Content;
                existingBlog.Tags = blogPost.Tags;

                 // Save the changes
                 await _blogyDbContext.SaveChangesAsync();

                //Show Success Message
                return existingBlog;
            }
            return null;
        }
    }
}