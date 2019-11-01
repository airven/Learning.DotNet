using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Threading;
//using SqlMonitor.TCDBMS.Core.Domain.EnumType;

namespace DeletegateTest.Redis
{
    public class RedisSource
    {
        private static RedisSource _instance;
        private static object _syncRoot = new Object();
        public static RedisSource GetClient(string env, string projectName)
        {
            var instance = _instance;
            if (instance == null)
            {
                lock (_syncRoot)
                {
                    instance = Volatile.Read(ref _instance);
                    if (instance == null)
                    {
                        instance = new RedisSource(env, projectName);
                    }
                    Volatile.Write(ref _instance, instance);
                }
            }
            return instance;
        }
        private ConfigCenter configcenter;
        public RedisSource(string env, string projectName)
        {
            configcenter = new ConfigCenter(env.ToLower(), projectName);
        }
        /// <summary>
        /// 获取ConnectionMultiplexer
        /// </summary>
        /// <param name="redisConfig">RedisConfig配置文件</param>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnect(string CacheName)
        {
            return configcenter.GetConnection(CacheName);
        }
        /// <summary>
        /// 获取数据库
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="db">默认为0：优先代码的db配置，其次config中的配置</param>
        /// <returns></returns>
        public IDatabase GetDatabase(string CacheName = null, int? db = null)
        {
            var connectplexer = GetConnect(CacheName);
            if (connectplexer == null)
                return null;
            return connectplexer.GetDatabase();
        }
        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect(configName).GetSubscriber();
        }
    }
}
