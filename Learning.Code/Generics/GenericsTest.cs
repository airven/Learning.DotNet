using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code.Generics
{
    public class GenericsTest : ITask
    {
        //泛型委托
        delegate T TestDeleate<T>(T t1, T t2);
        public string Name
        {
            get { return GetType().Name; }
        }
        public void Run()
        {
            TestDeleate<int> test = (a, b) => { return a + b; };
            string testx(string a, string b) { return a + b; }
            TestDeleate<string> test2 = new TestDeleate<string>(testx);
            Console.WriteLine(test(1, 2));
            Console.WriteLine(test2("hello", "airven2"));

            InvokeMethod(() => { Console.WriteLine("123"); });

            InvokeMethod(p => Console.WriteLine(p), "hello,welcome1");
            InvokeMethod((p) => Console.WriteLine(p), "hello,welcome2");
            InvokeMethod((p) => { Console.WriteLine(p); }, "hello,welcome3");


            Console.WriteLine(InvokeMethodFunc(p => string.Concat(new string[] { "a", p }), "fjkd"));
            Console.WriteLine(InvokeMethodFunc((p) => string.Concat(new string[] { "a", p }), "hello,welcome2"));
            Console.WriteLine(InvokeMethodFunc((p) => { return string.Concat(new string[] { "a", p }); }, "hello,welcome3"));



            try
            {

            }
            catch(Exception ex)
            {
                ExceptionDispatchInfo.Capture(ex).Throw();
            }
            Console.Read();
        }
        void InvokeMethod(Action action, string param = "")
        {
            action.Invoke();
        }
        void InvokeMethod(Action<string> action, string param = "")
        {
            if (string.IsNullOrEmpty(param))
                action("0");
            else action(param);
        }
        //泛型方法
        void InvokeMethod<T>(Action<T> action, T param)
        {
            action(param);
        }
        //泛型方法
        T InvokeMethodFunc<T>(Func<T, T> action, T param)
        {
            return action(param);
        }
    }
}
