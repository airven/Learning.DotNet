using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code
{
    class DelegateExample : ITask
    {
        public string Name
        {
            get { return GetType().Name; }
        }

        delegate string test();
        public void Run()
        {
            test ts = new test(TestDelegate);
            IAsyncResult result = ts.BeginInvoke(null, null); //会在新线程中执行
            string resultstr = ts.EndInvoke(result);
            Console.WriteLine(resultstr);
            Console.WriteLine("fjdkj");

            test ts1 = Show;
            Console.WriteLine(ts1);
        }

        string Show()
        {
            return "hello,welcome";
        }
        internal static string TestDelegate()
        {
            Thread.Sleep(1000);
            return "hello";
        }
    }
}
