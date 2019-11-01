using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    class ManualResetEventTest : ITask
    {
        public string Name
        {
            get { return GetType().Name; }
        }

        public void Run()
        {
            //测试ManualResetEvent
            var manualEvent = new ManualResetEvent(false);
            var mutipleThread = Enumerable.Range(0, 10).Select(i => Task.Factory.StartNew(() =>
            {
                manualEvent.WaitOne();
                Console.WriteLine("这是线程{0}", i);
                manualEvent.Reset();
            }, TaskCreationOptions.LongRunning)).ToArray();
            //Task.Factory.StartNew(() =>
            //{
            //    var i = 0;
            //    while (i < 10)
            //    {
            //        i++;
            //        manualEvent.Set();
            //    }
            //});
            manualEvent.Set();
            Task.WaitAll(mutipleThread);


            //var autoEvent = new AutoResetEvent(false);
            //var mutipleThread = Enumerable.Range(0, 10).Select(i => Task.Factory.StartNew(() =>
            //{
            //    autoEvent.Set();
            //    Console.WriteLine("这是线程{0}", i);
            //})).ToArray();

            //Task.WaitAll(mutipleThread);


        }
    }
}
