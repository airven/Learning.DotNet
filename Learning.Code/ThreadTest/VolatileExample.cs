using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    class VolatileExample : ITask
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
            ThreadCreate tr = new ThreadCreate();
            tr.ProcessInit();
        }
    }


    public class ThreadCreate
    {
        public void ProcessInit()
        {
            worker workObject = new worker();
            Thread workThread = new Thread(workObject.DoWork);

            workThread.Start();
            Console.WriteLine("主进程开始运行辅助线程");
            while (!workThread.IsAlive) ;

            //让主线程休息一会，目的是为了让辅助子线程充分的运行几个循环
            Thread.Sleep(10);

            workObject.ThreadStop();
            workThread.Join();

            Console.WriteLine("主线程运行提示，此时辅助线程运行完毕");
        }

    }

    public class worker
    {
        //volatile修饰符是为了表明该变量不会随其它线程单元改动(线程的原子性)
        private volatile bool _isRequestStop;

        public void DoWork()
        {
            while (!_isRequestStop)
            {
                Console.WriteLine("辅助线程运行中");
            }
            Console.WriteLine("辅助线程运行结束");
        }

        public void ThreadStop()
        {
            _isRequestStop = true;
        }
    }


}
