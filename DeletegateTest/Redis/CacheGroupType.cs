using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest.Redis
{
    public enum CacheGroupType
    {
        M,//主从
        S,//单机
        C,//集群
        P,//Redis Proxy
    }
}
