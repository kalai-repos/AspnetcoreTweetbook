using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Contract
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

       
        public Uri GetPostUri(string empId)
        {
            return new Uri(_baseUri + ApiRoutes.RequestUri.Get.Replace("{empId}", empId));
        }

       
    }
}
