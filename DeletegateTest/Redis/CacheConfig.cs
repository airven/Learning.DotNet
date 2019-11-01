using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest.Redis
{
   public class CacheConfig
    {
        public string name { get; set; }
        public CacheGroupType Type { get; set; }
        public List<RedisConfig> instances { get; set; }
        public CacheConfig()
        {
            instances = new List<RedisConfig>();
        }
    }
}
