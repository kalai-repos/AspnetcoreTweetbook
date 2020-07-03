using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Repository
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class
    {
        protected readonly DbContext DatabaseContext;

        public Repository(DbContext context)
        {
            this.DatabaseContext = context;
        }

        public async Task Add(TModel entity)
        {
            _ = DatabaseContext.Set<TModel>().AddAsync(entity);
            await DatabaseContext.SaveChangesAsync();
        }

        public async Task<TModel> Get(Guid id)
        {
            return await DatabaseContext.Set<TModel>().FindAsync(id);
        }

        public async Task<IEnumerable<TModel>> GetAll()
        {
            return await DatabaseContext.Set<TModel>().ToListAsync();
        }

        public void Remove(TModel entity)
        {
            DatabaseContext.Set<TModel>().Remove(entity);
        }
    }
}
