using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest.Redis
{
    public class RedisConfig
    {
        public string ip { get; set; }
        public string password { get; set; }
        public bool sentinel { get; set; }
    }
}
