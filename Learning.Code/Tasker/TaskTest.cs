using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.Tasker
{
    public class TaskTest : ITask
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
            Func<object, string> actiontest = (object obj) =>
            {
                Thread.Sleep(1000);
                var a = obj;
                return $"这是数据{a},当前线程id{Thread.CurrentThread.ManagedThreadId}";
            };

            Action action = async () =>
            {
                for (var i = 0; i < 2; i++)
                {
                    var msg = await Task.Factory.StartNew(actiontest, i);
                    Console.WriteLine(msg);
                }
            };

            Task[] testtask = new Task[1000];
            for (int i = 0; i < 1000; i++)
            {
                testtask[i] = Task.Factory.StartNew(action);
            }
            try
            {
                await Task.WhenAll(testtask);
                Console.WriteLine("jfdkjfdk");
            }
            catch (AggregateException exceptions)
            {
                foreach (var ex in exceptions.Flatten().InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }


            Func<object, int> action2 = (object obj) =>
            {
                int i = (int)obj;

                // Make each thread sleep a different time in order to return a different tick count
                Thread.Sleep(i * 100);

                // The tasks that receive an argument between 2 and 5 throw exceptions
                if (2 <= i && i <= 5)
                {
                    throw new InvalidOperationException("SIMULATED EXCEPTION");
                }

                int tickCount = Environment.TickCount;
                //Console.WriteLine("Task={0}, i={1}, TickCount={2}, Thread={3}", Task.CurrentId, i, tickCount, Thread.CurrentThread.ManagedThreadId);

                return tickCount;
            };


            Task[] testtaskg = new Task[10];
            for (int i = 0; i < 10; i++)
            {
                testtaskg[i] = Task.Factory.StartNew(action2, i);
            }
            try
            {
              await  Task.WhenAll(testtask);
            }
            catch (AggregateException e)
            {
                Console.WriteLine("\nThe following exceptions have been thrown by WaitAll(): (THIS WAS EXPECTED)");
                for (int j = 0; j < e.InnerExceptions.Count; j++)
                {
                    Console.WriteLine("\n-------------------------------------------------\n{0}", e.InnerExceptions[j].ToString());
                }
            }
            Console.WriteLine("test test test test");
            Console.ReadKey();
        }
    }
}
