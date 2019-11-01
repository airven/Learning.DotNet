using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeletegateTest
{
    /*委托的常见定义方式*/
    public class DeletegateStep1
    {
        //声明一个委托类型
        delegate void DeletegateTest(string str1, string str2);
        //声明一个带返回参数的委托
        delegate string GreetingsDelegate(string name);

        public void Show()
        {
            var a = "a";
            var b = "b";

            //通过new来建立委托的变量
            DeletegateTest test = new DeletegateTest((string x, string y) =>
            {
                Console.WriteLine(x);
                Console.WriteLine(y);
            });
            test(a, b);

            //使用逆名方法的委托实现委托实例
            DeletegateTest test2 = delegate (string x, string y)
            {
                Console.WriteLine(x);
                Console.WriteLine(y);
            };
            test2(a, b);

            //使用Lambda Expressions实现委托实例
            DeletegateTest test3 = (string x, string y) =>
            {
                Console.WriteLine(x);
                Console.WriteLine(y);
            };
            test3(a, b);

            //使用方法的注册来实例化委托
            DeletegateStep1 stepAction = new DeletegateStep1();
            GreetingsDelegate greetingDelegate = new GreetingsDelegate(stepAction.Greetings);
            GreetingsDelegate greetingDelegate2 = stepAction.Greetings;
            string result = greetingDelegate("welcome,tester");
            Console.WriteLine(result);
            result = greetingDelegate2("jack");
            Console.WriteLine(result);


            //string result1 = greetingDelegate.Invoke("welcome,tester");

            IAsyncResult asyncResult = greetingDelegate.BeginInvoke("welcome,tester", null, null);
            //调用EndInvoke方法获取异步执行的结果
            var invokeResult = greetingDelegate.EndInvoke(asyncResult);
            //BeginInvoke虽然是异步，但是仍然阻塞主线程
            Console.WriteLine(invokeResult);


            //通过回调函数来避免阻塞，而将整个结果的返回写在AsyncCallBack里面
            //这种可以达到异步的效果，不阻塞主线程
            greetingDelegate.BeginInvoke("welcome,airven", ExcuteCompleted, null);

            Console.WriteLine("end");
            Console.Read();
        }

        public void ExcuteCompleted(IAsyncResult IResult)
        {
            Thread.Sleep(5000);
            Console.WriteLine("AddComplete running on thread {0}", Thread.CurrentThread.ManagedThreadId);

            //获取绑定函数的引用
            AsyncResult ar = (AsyncResult)IResult;
            GreetingsDelegate delBp = (GreetingsDelegate)ar.AsyncDelegate;
            //等待函数执行完毕
            string result = delBp.EndInvoke(IResult);
            Console.WriteLine("5 + 5 ={0}", result);
        }

        public string Greetings(string name)
        {
            Thread.Sleep(3000);
            return "Hello @" + name;
        }
    }
}
