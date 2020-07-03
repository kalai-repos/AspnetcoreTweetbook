using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Contract
{
    public interface IUriService
    {
        Uri GetPostUri(string postId);

       // Uri GetAllPostsUri(PaginationQuery pagination = null);
    }
}
