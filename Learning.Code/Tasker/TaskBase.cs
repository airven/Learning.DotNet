using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.Tasker
{
    class TaskBase : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        public void Run()
        {
            try
            {
                Func<object, string> actiontest = (object obj) =>
                {
                    Thread.Sleep(100);
                    var b = obj;
                    return $"这是数据{b},当前线程id{Thread.CurrentThread.ManagedThreadId}";
                };

                Action action = async () =>
                {
                    for (var i = 0; i < 100; i++)
                    {
                        var msg = await System.Threading.Tasks.Task.Factory.StartNew(actiontest, i);
                        Console.WriteLine(msg);
                    }
                };

                //Action<object> ac = (t) => { };
                System.Threading.Tasks.Task[] testtask = new System.Threading.Tasks.Task[100];
                for (int i = 0; i < 100; i++)
                {
                    testtask[i] = System.Threading.Tasks.Task.Factory.StartNew(action);
                    //testtask[i] = System.Threading.Tasks.Task.Factory.StartNew(ac,i);
                }
                var a = System.Threading.Tasks.Task.WhenAll(testtask);
                a.Wait();
            }
            catch (AggregateException exceptions)
            {
                foreach (var ex in exceptions.Flatten().InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
