using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Contract;
using TweeterBook.Contract.Request;
using TweeterBook.Contract.Response;
using TweeterBook.Domain;
using TweeterBook.Extension;
using TweeterBook.Repository;

namespace TweeterBook.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeesController : ParentController<Employee, EmployeeRepository>
    {
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public EmployeesController(EmployeeRepository empRepository, IMapper _mapper, IUriService uriService) : base(empRepository)
        {
            this._mapper = _mapper;
            this._uriService = uriService;
        }


        [HttpGet(ApiRoutes.RequestUri.getAll)]
        public async Task<IEnumerable<Employee>> AllEmployees()
        {
            return await Repository.GetEmployees();
        }

        [HttpGet(ApiRoutes.RequestUri.Get)]
        public async Task<IActionResult> Get([FromRoute] Guid empId)
        {
            Employee post = await Repository.Get(empId); ;//_postService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(post)));

        }

        /// <summary>
        /// creating the new Employee
        /// </summary>    
        [HttpPost(ApiRoutes.RequestUri.Create)]
        public async Task<IActionResult> Create([FromBody] CreatePostRequest postRequest)
        {
            Guid newPostId = Guid.NewGuid();
            Employee post = new Employee
            {
                Id = newPostId,
                Name = postRequest.Name,
                UserId = HttpContext.GetUserId(),
                Tags = postRequest.Tags.Select(x => new EmployeeTag { EmpId = newPostId, TagName = x }).ToList()
            };

            await Repository.CreatePostAsync(post);

            Uri locationUri = _uriService.GetPostUri(post.Id.ToString());
            return Created(locationUri, new Response<PostResponse>(_mapper.Map<PostResponse>(post)));
        }


        [HttpPut(ApiRoutes.RequestUri.Update)]
        public async Task<IActionResult> Update([FromRoute] Guid postId, [FromBody] UpdatePostRequest request)
        {
            bool userOwnsPost = await Repository.UserOwnsPostAsync(postId, HttpContext.GetUserId());

            if (!userOwnsPost)
            {
                return BadRequest(error: new ErrorResponse(new ErrorModel { Message = "You do not own this post" }));
            }

            Employee employee = await Repository.Get(postId);
            employee.Name = request.Name;

            bool updated = await Repository.UpdatePostAsync(employee);

            if (updated)
            {
                return Ok(new Response<PostResponse>(_mapper.Map<PostResponse>(employee)));
            }

            return NotFound();
        }

    }
}
