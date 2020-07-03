using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Contract
{  
    public static  class ApiRoutes
    {

        public const string version = "v1";
        public const string root = "api";

        public const string Base = root+"/"+version;

        public static class RequestUri
        {
            public const string getAll = Base + "/getname";

            public const string Create = Base + "/posts";

            public const string Get = Base + "/getemployee/{empId}";

            public const string Update = Base + "/updateEmployee/{postId}";

        }

        public static class Tags
        {
            public const string GetAll = Base + "/tags";

            public const string Get = Base + "/tags/{tagName}";

            public const string Create = Base + "/tags";

            public const string Delete = Base + "/tags/{tagName}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";

            public const string Register = Base + "/identity/register";

            public const string Refresh = Base + "/identity/refresh";
        }


    }
}
