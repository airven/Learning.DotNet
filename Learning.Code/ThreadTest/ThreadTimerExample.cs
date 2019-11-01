using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    public class ThreadTimerExample : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }
        static AutoResetEvent resetevent = new AutoResetEvent(true);
        //static ManualResetEvent resetevent = new ManualResetEvent(true);
        public void Run()
        {

            var thread = new System.Threading.Thread(() =>
             {
                 while (true)
                 {
                     Action a = () =>
                     {
                         Console.WriteLine("abc");
                     };
                     a.Invoke();
                     System.Threading.Thread.Sleep(5 * 1000);
                 }

             });
            thread.Start();
            ConcurrentDictionary<string, string> pool = new ConcurrentDictionary<string, string>();
            pool.AddOrUpdate("1", "a", (a, b) => { return b + 1; });
            pool.AddOrUpdate("1", "a", (a, b) => { return b + 1; });
            pool.AddOrUpdate("1", "a", (a, b) => { return b + 1; });





            Thread myReaderThread = new Thread(new ThreadStart(MyReadThreadProc));
            myReaderThread.Start();

            resetevent.Set();
            Action a1 = () =>
            {
                Console.WriteLine("abc");
            };


            var schedule = new DataBaseSchedule(a1, 5 * 1000);
            schedule.Start();
        }

        void MyReadThreadProc()
        {
            while(true)
            {
                resetevent.WaitOne();
                Console.WriteLine("fjkdfjksdfjds");
                Thread.Sleep(1000);
                resetevent.Set();
            } 
        }
    }


    public class DataBaseSchedule
    {
        public bool running { get; set; }
        System.Threading.Thread thread;
        public DataBaseSchedule(Action action, int timeout)
        {
            thread = new System.Threading.Thread(() =>
            {
                while (running)
                {
                    action.Invoke();
                    System.Threading.Thread.Sleep(timeout);
                }

            });
        }

        public void Start()
        {
            running = true;
            thread.Start();
        }

        public void Stop()
        {
            running = false;
        }

    }
}
