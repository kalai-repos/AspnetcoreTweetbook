using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Contract;
using TweeterBook.Contract.Response;
using TweeterBook.Repository;

namespace TweeterBook.Controllers
{
    public class TagsController : Controller
    {
       
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _empRepository;

        public TagsController(EmployeeRepository empRepository, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._empRepository = empRepository;
        }


        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "Tagviewer")]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _empRepository.GetAllTagsAsync();
            return Ok(_mapper.Map<List<TagResponse>>(tags));
        }

        [HttpGet(ApiRoutes.Tags.Get)]
        public async Task<IActionResult> Get([FromRoute] string tagName)
        {
            var tag = await _empRepository.GetTagByNameAsync(tagName);

            if (tag == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TagResponse>(tag));
        }


    }
}
