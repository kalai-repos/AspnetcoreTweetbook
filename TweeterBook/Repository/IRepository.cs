using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Repository
{
    public interface IRepository<TModel> where TModel : class
    {
        Task<TModel> Get(Guid id);
        Task<IEnumerable<TModel>> GetAll();
        Task Add(TModel entity);
        void Remove(TModel entity);
    }
}
