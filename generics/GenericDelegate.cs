using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics
{
    /// <summary>
    /// action与func的区别在于是否返回参数
    /// </summary>
    class GenericDelegate : ITask
    {
        public void Print(Action<string> action)
        {
            action("this is test action");
        }
        public void Print(string Message, Action<string> action)
        {
            action(Message);
        }
        public void Print<T>(T Message, Action<T> action)
        {
            action(Message);
        }

        /// <summary>
        /// 计算
        /// </summary>
        /// <typeparam name="T">泛型T</typeparam>
        /// <param name="t1">参数t1</param>
        /// <param name="t2">参数t2</param>
        /// <param name="action">委托方法</param>
        /// <returns>返回参果Tresult</returns>
        public T Print<T>(T t1, T t2, Func<T, T, T> action)
        {
            return action(t1, t2);
        }

        public void Show()
        {
            Print((a) => { Console.WriteLine(a); });
            Print("hello,welcome!", (a) => { Console.WriteLine(a); });
            Print("hello,jack!", delegate (string a) { Console.WriteLine(a); });
            var result = Print(1, 2, (a, b) => a + b);
            Console.WriteLine(result);
        }
    }
}
