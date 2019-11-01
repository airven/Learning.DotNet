using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    public class ThreadExample : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }
        //0 for false, 1 for true.
        private static int usingResource = 0;

        private const int numThreadIterations = 5;
        private const int numThreads = 10;


       static AutoResetEvent resetEvent = new AutoResetEvent(true);
        public void Run()
        {
            /*Thread myThread;
            Random rnd = new Random();

            for (int i = 0; i < numThreads; i++)
            {
                myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);

                //Wait a random amount of time before starting next thread.
                Thread.Sleep(rnd.Next(0, 1000));
                myThread.Start();
                student.show();
            }*/


            /*System.Threading.Thread t1 = new System.Threading.Thread(() =>
            {
                Console.WriteLine("fsdjfksdjfksdjfsd");
            });

            System.Threading.Thread t2 = new System.Threading.Thread(delegate ()
            {
                Console.WriteLine("1545");
            });
            t1.Start();
            t2.Start();

            Action a1 = delegate ()
            {
                Console.WriteLine("23456");
            };
            a1();
            DataBaseSchedule schedule2 = new DataBaseSchedule(a1, 5 * 1000);
            schedule2.Start();
            Action a2 = () =>
            {
                Console.WriteLine("5687878");
            };
            a2();*/



            //Thread[] myThread=new Thread[100];
            //for (int i = 0; i < 100; i++)
            //{
            //    myThread[i] = new Thread(new ThreadStart(ActionMethod));
            //}
            //for (int i = 0; i < myThread.Length; i++)
            //{
            //    myThread[i].Name = "子线程" + i;
            //    myThread[i].Start();
            //}


            //Thread t = new Thread(WaitFoSingalToWrite);
            //t.Start();
            //Thread.Sleep(3000);
            ////resetEvent.Set();
            //Console.WriteLine("Main End...");
            int numberOfActions = 10;
            using (ManualResetEvent mre = new ManualResetEvent(false))
            {

                Console.WriteLine("fjdkfj");
                for (int i = 0; i < numberOfActions; i++)
                {
                    ThreadPool.QueueUserWorkItem((o) =>
                    {
                        Thread.SpinWait(5000);
                        if (Interlocked.Decrement(ref numberOfActions) == 0)
                        {
                            mre.Set();
                        }                        
                    },
                        null);
                }
                mre.WaitOne();
            }



            //resetEvent.Set();
            Console.Read();
        }

        static object ob = new object();
        public void ActionMethod()
        {
            //resetEvent.WaitOne();
            lock(ob)
            { 
            Thread.Sleep(500);
            Console.WriteLine("线程名：" + Thread.CurrentThread.Name);
            }
        }
        void WaitFoSingalToWrite()
        {
            Console.WriteLine("in...");
            resetEvent.WaitOne();
            Console.WriteLine("do sth...");
            resetEvent.WaitOne(5000);
            Console.WriteLine("do sth 1...");
            Console.WriteLine("out...");
        }
        private static void MyThreadProc()
        {
            for (int i = 0; i < numThreadIterations; i++)
            {
                student.show();

                //Wait 1 second before next attempt.
                Thread.Sleep(1000);
            }
        }
        //A simple method that denies reentrancy.
        static bool UseResource()
        {
            //0 indicates that the method is not in use.
            if (0 == Interlocked.Exchange(ref usingResource, 1))
            {
                Console.WriteLine("{0} acquired the lock", Thread.CurrentThread.Name);

                //Code to access a resource that is not thread safe would go here.

                //Simulate some work
                Thread.Sleep(500);

                Console.WriteLine("{0} exiting lock", Thread.CurrentThread.Name);

                //Release the lock
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else
            {
                Console.WriteLine("   {0} was denied the lock", Thread.CurrentThread.Name);
                return false;
            }
        }

    }

    public class student
    {
        static int a = 0;
        static student()
        {
            a++;
        }
        public static void show()
        {
            Console.WriteLine(a);
        }
    }

}
