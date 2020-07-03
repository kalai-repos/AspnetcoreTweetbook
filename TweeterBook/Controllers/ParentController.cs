using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Repository;

namespace TweeterBook.Controllers
{
    public abstract class ParentController<TModel, TRepository> : ControllerBase where TModel : class where TRepository : IRepository<TModel>
    {
        protected readonly TRepository Repository;

        public ParentController(TRepository repository)
        {
            Repository = repository;
        }

        [HttpGet]
        public async  Task<IEnumerable<TModel>> Get()
        {
            return await Repository.GetAll();
        }

        [HttpPost]
        public void Add([FromBody] TModel item)
        {
            Repository.Add(item);
        }

    }
}
