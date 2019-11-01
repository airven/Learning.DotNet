using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code.Generics
{
    class GenericsMethod : ITask
    {
        public string Name
        {
            get { return GetType().Name; }
        }

        public void Run()
        {
            //测试一个无返回值的泛型方法
            int param1 = 100;
            string param2 = "hello,welcome!";
            TestTParam(param1);
            TestTParam(param2);

            //测试一个有返回值的泛型方法
            Console.WriteLine(TestTReturn(1, 2));
            Console.WriteLine(TestTReturn("1", "2"));

            //泛型类
            int param3 = 100;
            string param4 = "hello,welcome!";
            new Calculate<int>().TestTParam(param3);
            new Calculate<string>().TestTParam(param4);

        }

        public void TestTParam<T>(T t1)
        {
            Console.WriteLine("type is {0},value is {1}", t1.GetType().ToString(), t1.ToString());
        }

        public T TestTReturn<T>(T t1, T t2)
        {
            if (t1 is int && t2 is int)
            {
                return t2;
            }
            else
            {
                return default(T);
            }
        }
    }


    class Calculate<T>
    {
        public void TestTParam(T t1)
        {
            Console.WriteLine("type is {0},value is {1}", t1.GetType().ToString(), t1.ToString());
        }

        public T TestTReturn(T t1, T t2)
        {
            if (t1 is int && t2 is int)
            {
                return t2;
            }
            else
            {
                return default(T);
            }
        }
    }
}
