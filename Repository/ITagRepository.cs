using Blogy_MVC.Models.Domain;

namespace Blogy_MVC.Repository
{
    public interface ITagRepository
    {
       Task<IEnumerable<Tag>> GetAllAsync();
       Task<Tag?> GetAsync(Guid id);
       Task<Tag> AddAsync(Tag tag);
       Task<Tag?> UpdateAsync(Tag tag);
       Task<Tag?> DeleteAsync(Guid id);

    }
}
