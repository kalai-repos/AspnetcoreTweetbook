using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweeterBook.Caching
{
    public class RadisCacheSetting
    {
        public bool Enabled { get; set; }

        public string ConnectionString { get; set; }
    }
}
