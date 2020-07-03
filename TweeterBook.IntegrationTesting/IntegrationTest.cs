using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TweeterBook.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TweeterBook.Contract;
using TweeterBook.Contract.Response;

namespace TweeterBook.IntegrationTesting
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient testClient;
        private readonly IServiceProvider _serviceProvider;

        public IntegrationTest()
        {
            var appfactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DataContext));
                        services.AddDbContext<DataContext>(options => { options.UseInMemoryDatabase("sampledb"); });
                    });

                });

            _serviceProvider = appfactory.Services;
            testClient = appfactory.CreateClient();
        }


        protected async Task AuthenticateAsync()
        {
            testClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", await GetJwtAsync());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await testClient.PostAsJsonAsync(ApiRoutes.Identity.Register, new UserRegistrationRequest
            {
                Email = "newuser@example.com",
                Password = "Lakshmi@10"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;

        }

        protected async Task<PostResponse> CreatePostAsync(CreatePostRequest request)
        {
            var response = await testClient.PostAsJsonAsync(ApiRoutes.RequestUri.Create, request);
            return (await response.Content.ReadAsAsync<Response<PostResponse>>()).Data;
        }


        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataContext>();
            context.Database.EnsureDeleted();
        }
    }
}
