using Blogy_MVC.Data;
using Blogy_MVC.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Blogy_MVC.Repository
{
    public class TagRepository : ITagRepository
    {
        private readonly BlogyDbContext _blogyDbContext;
        public TagRepository(BlogyDbContext blogyDbContext)
        {
          _blogyDbContext = blogyDbContext;
        }
        
        public async Task<Tag> AddAsync(Tag tag)
        {
        await _blogyDbContext.Tags.AddAsync(tag);
        await _blogyDbContext.SaveChangesAsync();
        return tag;

        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
          var existingTag = await _blogyDbContext.Tags.FindAsync(id);
          if (existingTag != null)
          {
            _blogyDbContext.Tags.Remove(existingTag);
            await _blogyDbContext.SaveChangesAsync();
            return existingTag;
          }
          return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
           return await _blogyDbContext.Tags.ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await _blogyDbContext.Tags.FirstOrDefaultAsync(x => x.Id == id);
        
        }
        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await _blogyDbContext.Tags.FindAsync(tag.Id);
             if (existingTag != null)
            {
                existingTag.Name = tag.Name;
                existingTag.DisplayName = tag.DisplayName;

                // Save the changes
                 await _blogyDbContext.SaveChangesAsync();

                //Show Success Message
                return existingTag;
            }
            return null;

        }

  
    }
}