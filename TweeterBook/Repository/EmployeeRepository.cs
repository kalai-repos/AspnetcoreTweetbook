using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Data;
using TweeterBook.Domain;

namespace TweeterBook.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public DataContext ApplicationDatabaseContext
        {
            get { return DatabaseContext as DataContext; }
        }

        public EmployeeRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await ApplicationDatabaseContext.Employees.ToArrayAsync();
        }


        public async Task<bool> CreatePostAsync(Employee post)
        {
            post.Tags?.ForEach(x => x.TagName = x.TagName.ToLower());

            await AddNewTags(post);
            await ApplicationDatabaseContext.Employees.AddAsync(post);

            var created = await ApplicationDatabaseContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<bool> UpdatePostAsync(Employee postToUpdate)
        {
            postToUpdate.Tags?.ForEach(x => x.TagName = x.TagName.ToLower());
            await AddNewTags(postToUpdate);
            ApplicationDatabaseContext.Employees.Update(postToUpdate);
            var updated = await ApplicationDatabaseContext.SaveChangesAsync();
            return updated > 0;
        }

        public async Task<bool> UserOwnsPostAsync(Guid postId, string userId)
        {
            var post = await ApplicationDatabaseContext.Employees.AsNoTracking().SingleOrDefaultAsync(x => x.Id == postId);

            if (post == null)
            {
                return false;
            }

            if (post.UserId != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<List<Tag>> GetAllTagsAsync()
        {
            return await ApplicationDatabaseContext.Tags.AsNoTracking().ToListAsync();
        }

        public async Task<bool> CreateTagAsync(Tag tag)
        {
            tag.Name = tag.Name.ToLower();
            var existingTag = await ApplicationDatabaseContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Name == tag.Name);
            if (existingTag != null)
                return true;

            await ApplicationDatabaseContext.Tags.AddAsync(tag);
            var created = await ApplicationDatabaseContext.SaveChangesAsync();
            return created > 0;
        }

        public async Task<Tag> GetTagByNameAsync(string tagName)
        {
            return await ApplicationDatabaseContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Name == tagName.ToLower());
        }

        public async Task<bool> DeleteTagAsync(string tagName)
        {
            var tag = await ApplicationDatabaseContext.Tags.AsNoTracking().SingleOrDefaultAsync(x => x.Name == tagName.ToLower());

            if (tag == null)
                return true;

            var postTags = await ApplicationDatabaseContext.EmployeeTags.Where(x => x.TagName == tagName.ToLower()).ToListAsync();

            ApplicationDatabaseContext.EmployeeTags.RemoveRange(postTags);
            ApplicationDatabaseContext.Tags.Remove(tag);
            return await ApplicationDatabaseContext.SaveChangesAsync() > postTags.Count;
        }

        private async Task AddNewTags(Employee post)
        {
            foreach (var tag in post.Tags)
            {
                var existingTag =
                    await ApplicationDatabaseContext.Tags.SingleOrDefaultAsync(x =>
                        x.Name == tag.TagName);
                if (existingTag != null)
                    continue;

                await ApplicationDatabaseContext.Tags.AddAsync(new Tag
                { Name = tag.TagName, CreatedOn = DateTime.UtcNow, CreatorId = post.UserId });
            }
        }

    }
}
