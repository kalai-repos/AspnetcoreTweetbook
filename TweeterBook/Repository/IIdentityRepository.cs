using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Domain;

namespace TweeterBook.Repository
{
    public interface IIdentityRepository
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);

        Task<AuthenticationResult> LoginAsync(string email, string password);

       Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken);


    }
}
