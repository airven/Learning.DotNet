using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
namespace Learning.Code.ThreadTest
{
    public class ParallelExample : ITask
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
                Parallel.For(0, 10, item =>
                {
                    throw new AggregateException(item.ToString());
                });
            }
            catch (AggregateException exceptions)
            {
                foreach (var item in exceptions.InnerExceptions)
                {
                    Console.WriteLine(item.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("jfsdfkjskdjfsdfsdfsdf");
            }


            //try
            //{
            //    Parallel.For(0, 10, item =>
            //    {
            //        var a = item / 0;
            //    });
            //}
            //catch (AggregateException exceptions)
            //{
            //    exceptions.Handle((inner) =>
            //    {
            //        if (inner is OperationCanceledException)
            //        {
            //            Console.WriteLine(inner.Message);
            //            return true;
            //        }
            //        else if (inner is NotImplementedException)
            //        {
            //            Console.WriteLine(inner.Message);
            //            return false;
            //        }
            //        else
            //        {
            //            Console.WriteLine(inner.Message);
            //            return true;
            //        }
            //    });
            //}

            //Task testtask = new Task(() =>
            //{
            //    foreach (var i in new int[1, 2, 3, 4, 5, 67])
            //    {
            //        Console.WriteLine(i / 0);
            //    }
            //});
            //testtask.Start();
            ////专门处理异常
            //testtask.ContinueWith((t) =>
            //{
            //    AggregateException exceptions = t.Exception;
            //    foreach (var item in exceptions.InnerExceptions)
            //    {
            //        Console.WriteLine(item.Message);
            //    }
            //});


            //AutoResetEvent resetevent = new AutoResetEvent(false);
            //Semaphore semap = new Semaphore(10, 200);
            //Action a = new Action(()=> {
            //    Task.Factory.StartNew(()=> {
            //        Parallel.For(1, 1000, (item) => {
            //            if(semap.WaitOne(500))
            //            {
            //                //resetevent.WaitOne();
            //                Console.WriteLine(item);
            //                //resetevent.Set();
            //                System.Threading.Thread.Sleep(1000);
            //                semap.Release();
            //            }
            //        });
            //    });
            //});
            //a.Invoke();
            //resetevent.Set();

            //Task task = Task.Run(() => {

            //    foreach (var i in new int[1, 2, 3, 4, 5, 67])
            //    {
            //        Console.WriteLine(i / 0);
            //    }
            //});
            //task.ContinueWith((t) =>
            //{
            //    AggregateException exceptions = t.Exception;
            //    foreach (var item in exceptions.InnerExceptions)
            //    {
            //        Console.WriteLine(item.Message);
            //    }
            //});

        }
    }
}
