using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Polly;
using System.Net.Http;
using System.Net;
using System.Collections;
using System.Net.Http.Headers;

namespace Learning.PollyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //一个简单的异常重试,按指定时间进行重试
            TestSimple();

            //一个简单的异常重试,按指定次数进行重试
            RetryCountTest();

            //模仿一个http的post请求，如果请求异常，则进行重试
            RetryInHttpRequest();
        }


        static void TestSimple()
        {
            try
            {
                var politicaWaitAndRetry = Policy
                    .Handle<Exception>()
                    .WaitAndRetry(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(7)
                    }, ReportaError);
                politicaWaitAndRetry.Execute(() =>
                {
                    return new Exception("测试一个简单的异常重试");
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Executed Failed,Message:({e.Message})");
            }
        }
        static void ReportaError(Exception e, TimeSpan tiempo, int intento, Context contexto)
        {
            Console.WriteLine($"异常: {intento:00} (调用秒数: {tiempo.Seconds} 秒)\t执行时间: {DateTime.Now}");
        }

        static void RetryCountTest()
        {
            try
            {
                var retryTwoTimesPolicy =
                     Policy
                         .Handle<DivideByZeroException>()
                         .Retry(3, (ex, count) =>
                         {
                             Console.WriteLine("执行失败! 重试次数 {0}", count);
                             Console.WriteLine("异常来自 {0}", ex.GetType().Name);
                         });
                retryTwoTimesPolicy.Execute(() =>
                {
                    Console.WriteLine("我执行了多少次");
                    var a = 0;
                    return 1 / a;
                });
            }
            catch (DivideByZeroException e)
            {
                Console.WriteLine($"Excuted Failed,Message: ({e.Message})");

            }
        }

        static void RetryInHttpRequest()
        {
            var _httpRequestPolicy = Policy.HandleResult<HttpResponseMessage>(
            r => r.StatusCode == HttpStatusCode.OK)
            .WaitAndRetryAsync(new[]
                 {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(3),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(7)
                 }, onRetry);

            try
            {
                Task.Run(async () =>
                {
                    HttpResponseMessage httpResponse = await _httpRequestPolicy.ExecuteAsync(async () =>
                    {
                        using (var client = new HttpClient(new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip }))
                        {
                            var postdata_1 = new Hashtable(){
                               {"appIds",new string[]{"102259"} },
                                {"pageSize",20},
                                { "pageNum",1},
                                { "endTime",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")},
                                { "beginTime", DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss.fff")}
                                };
                            client.Timeout = new TimeSpan(0, 0, 10);
                            client.DefaultRequestHeaders.Add("token", "a2568b6b-aa13-4908-81a7-0446c4764f18");
                            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            var buffer = JsonConvert.SerializeObject(postdata_1);
                            var a = await client.PostAsync("http://sky.dss.com/log/real/list", new StringContent(buffer, Encoding.UTF8, "application/json"));
                            var b = a.StatusCode;
                            return a;
                        }
                    });

                    var result = await httpResponse.Content.ReadAsStringAsync();
                    Console.WriteLine("查询结果{0}", result);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        static Action<DelegateResult<HttpResponseMessage>, TimeSpan> onRetry = (a, b) =>
        {
            Console.WriteLine("时间{0},状态码{1},结果{2}", b.Milliseconds, a.Result.StatusCode, a.Result.Content.ReadAsStringAsync());
        };
    }
}
