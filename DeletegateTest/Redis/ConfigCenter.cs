using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeletegateTest.Redis
{
    public class ConfigCenter
    {
        public string Env { get; set; }
        public string ProjectName { get; set; }
        private static string _httpUriPattern = "http://tccomponent.17usoft.com/tcconfigcenter6/v6/getspecifickeyvalue/{0}/{1}/TCBase.Cache.v2";
        private List<CacheConfig> redisConfigs = new List<CacheConfig>();
        private BlockingDictionary<string, ConnectionMultiplexer> _connections;
        internal ConfigCenter(string Env, string ProjectName)
        {
            this.Env = Env;
            this.ProjectName = ProjectName;
            _connections = new BlockingDictionary<string, ConnectionMultiplexer>();
            SetConfig();
            Task.Run(() => ConfigWatch());
        }
        private static ManualResetEventSlim manualResetEventSlim;
        private async Task ConfigWatch()
        {
            for (int i = 0; i < int.MaxValue; i++)
            {
                SetConfig();
                await Task.Delay(30 * 1000);
            }
        }
        private void SetConfig()
        {
            manualResetEventSlim = new ManualResetEventSlim(false);
            var task = Task.Run(async () =>
            {
                try
                {
                    var configs = await getCacheConfigs();
                    redisConfigs = configs;
                }
                finally
                {
                    manualResetEventSlim.Set();
                }
            });
            manualResetEventSlim.Wait();
            task.Wait();
        }
        private async Task<List<CacheConfig>> getCacheConfigs()
        {
            var configstring = await GetConfigString(this.Env, this.ProjectName);
            return JsonConvert.DeserializeObject<List<CacheConfig>>(configstring);
        }
        private async Task<string> GetConfigString(string env, string projectName)
        {
            var username = "02723";
            var password = "gLJNRzjt";
            var usernamePassword = username + ":" + password;
            using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
            {
                try
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));
                    var response = await client.GetAsync(string.Format(_httpUriPattern, env, projectName));
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        private List<CacheConfig> GetCacheConfig(string dataBaseName)
        {
            return redisConfigs==null?null: redisConfigs.Where(item => item.name.Equals(dataBaseName)).ToList();
        }
        public ConnectionMultiplexer GetConnection(string dataBaseName)
        {
            List<CacheConfig> cacheList =  GetCacheConfig(dataBaseName);
            if (cacheList!=null&&cacheList.Count>0)
            {
                var cacheConfig = cacheList[new Random().Next(cacheList.Count)];
                var redisConfig = cacheConfig.instances.FirstOrDefault();
                if (redisConfig == null)
                {
                    var msg = $"名为{cacheConfig.name}的cache节点未配置任何redis服务器节点，请联系BPS";
                    throw new Exception(msg);
                }
                return _connections.GetOrAdd(cacheConfig.name, p => CreateConnection(cacheConfig));
            }
            else
            {
                return null;
            }
        }
        private ConnectionMultiplexer CreateConnection(CacheConfig cacheConfig)
        {
            var ipList = string.Join(",", cacheConfig.instances.Select(p => p.ip).ToArray());
            var options = ConfigurationOptions.Parse(ipList);
            options.ConnectTimeout = 1000 * 15;
            options.KeepAlive = 15;
            options.ResolveDns = false;
            options.AbortOnConnectFail = false;
            options.ClientName = $"{ProjectName}";
            var conn = ConnectionMultiplexer.Connect(options);
            return conn;
        }
    }
}
