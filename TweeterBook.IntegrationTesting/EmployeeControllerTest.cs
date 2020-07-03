using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TweeterBook.Contract;
using TweeterBook.Contract.Response;
using TweeterBook.Domain;
using Xunit;

namespace TweeterBook.IntegrationTesting
{
    public class EmployeeControllerTest: IntegrationTest
    {
        [Fact]
        public async Task GetAll_WithoutAnyPosts_ReturnEmptyRegistration()
        {
            await AuthenticateAsync();

            var response = await testClient.GetAsync(ApiRoutes.RequestUri.getAll);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            (await response.Content.ReadAsAsync<List<Employee>>()).Should().BeEmpty();
        }

        [Fact]
        public async Task Get_ReturnsPost_WhenPostExistsInTheDatabase()
        {
            // Arrange
            await AuthenticateAsync();
            var createdPost = await CreatePostAsync(new CreatePostRequest
            {
                Name = "Test post"
               
            });

            // Act
            var response = await testClient.GetAsync(ApiRoutes.RequestUri.Get.Replace("{empId}", createdPost.Id.ToString()));

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var returnedPost = await response.Content.ReadAsAsync<Response<PostResponse>>();
            returnedPost.Data.Id.Should().Be(createdPost.Id);
            returnedPost.Data.Name.Should().Be("Test post");
          
        }


    }
}
