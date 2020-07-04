using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Caching;
using TweeterBook.Contract;
using TweeterBook.Contract.Request;
using TweeterBook.Contract.Response;
using TweeterBook.Domain;
using TweeterBook.Extension;
using TweeterBook.Repository;

namespace TweeterBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin,User")]
    [Produces("application/json")]
    public class TagsController : Controller
    {
       
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _empRepository;

        public TagsController(EmployeeRepository empRepository, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._empRepository = empRepository;
        }

        [Cached(300)]
        [HttpGet(ApiRoutes.Tags.GetAll)]
        [Authorize(Policy = "MustworkwithDomain")]
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

        /// <summary>
        /// Creating the tag
        /// </summary>
        /// <response code="201">Creates a tag in the system</response>
        /// <response code="400">Unable to create the tag due to validation error</response>
        [HttpPost(ApiRoutes.Tags.Create)]
        [ProducesResponseType(typeof(TagResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateTagRequest request)
        {
            var newTag = new Tag
            {
                Name = request.TagName,
                CreatorId = HttpContext.GetUserId(),
                CreatedOn = DateTime.UtcNow
            };

            var created = await _empRepository.CreateTagAsync(newTag);
            if (!created)
            {
                return BadRequest(new ErrorResponse(new ErrorModel { Message = "Unable to create tag" }));
            }

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var locationUri = baseUrl + "/" + ApiRoutes.Tags.Get.Replace("{tagName}", newTag.Name);
            return Created(locationUri, _mapper.Map<TagResponse>(newTag));
        }


    }
}
