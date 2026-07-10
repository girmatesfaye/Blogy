using Blogy_MVC.Data;
using Blogy_MVC.Models.Domain;

namespace Blogy_MVC.Repository
{
  public interface IBlogPostRepository
{
    Task<IEnumerable<BlogPost>> GetAllAsync();
    Task<BlogPost?> GetAsync(Guid id);
    Task<BlogPost> AddAsync(BlogPost blogPost);
    Task<BlogPost?> UpdateAsync(BlogPost blogPost);
    Task<BlogPost?> DeleteAsync(Guid id);
}  
}


