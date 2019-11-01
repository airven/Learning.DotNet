using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    class ConfigureAwaitTest : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        public async void Run()
        {
            var threadid1 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(threadid1);

            Func<object, int> calctest = (object a) => { Thread.Sleep(1000); return int.Parse(a.ToString()); };
            for (int i = 0; i < 100; i++)
            {
               var a= await Task.Factory.StartNew(calctest, i.ToString()).ConfigureAwait(false);
                Console.WriteLine($"这是参数{a},进程id{Thread.CurrentThread.ManagedThreadId}");
            }
            var threadid2 = Thread.CurrentThread.ManagedThreadId;
            Console.WriteLine(threadid2);
        }

        public async static Task<string> GetUlrString(string url)
        {
            using (HttpClient http = new HttpClient())
            {
                var threadid1 = Thread.CurrentThread.ManagedThreadId;
                await http.GetStringAsync(url).ConfigureAwait(false);
                var threadid2 = Thread.CurrentThread.ManagedThreadId;
                return "";
            }
        }
    }
}
