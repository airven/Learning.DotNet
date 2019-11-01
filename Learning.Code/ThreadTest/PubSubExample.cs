using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    class PubSubExample : ITask
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
            //运行ThreadSync，演示线程的同步
            ThreadSync tsync = new ThreadSync();
            tsync.ThreadShow();
        }
    }



    class ThreadSync
    {
        private void ShowQueueContents(Queue<int> q)
        {
            lock (((ICollection)q).SyncRoot)
            {
                foreach (int item in q)
                {
                    Console.Write("{0} ", item);
                }
            }
            Console.WriteLine();
        }

        public void ThreadShow()
        {
            Queue<int> queue = new Queue<int>();
            SyncEvents syncEvents = new SyncEvents();

            Console.WriteLine("Configuring worker threads...");
            Procedure producer = new Procedure(queue, syncEvents);
            Consumer consumer = new Consumer(queue, syncEvents);

            Thread producerThread = new Thread(producer.ThreadRun);
            Thread consumerThread = new Thread(consumer.ThreadRun);

            Console.WriteLine("Launching producer and consumer threads...");
            producerThread.Start();
            consumerThread.Start();

            for (int i = 0; i < 4; i++)
            {
                Thread.Sleep(2500);
                ShowQueueContents(queue);
            }

            Console.WriteLine("Signaling threads to terminate...");
            syncEvents.ExitThreadEvent.Set();

            producerThread.Join();
            consumerThread.Join();
        }
    }

    class SyncEvents
    {

        public SyncEvents()
        {
            _exitThreadEvent = new ManualResetEvent(false);
            _newItemEvent = new AutoResetEvent(false);

            _eventArray = new WaitHandle[2];
            _eventArray[0] = NewItemEvent;
            _eventArray[1] = _exitThreadEvent;
        }
        private EventWaitHandle _newItemEvent;

        public EventWaitHandle NewItemEvent
        {
            get { return _newItemEvent; }
        }
        private EventWaitHandle _exitThreadEvent;

        public EventWaitHandle ExitThreadEvent
        {
            get { return _exitThreadEvent; }
        }
        private WaitHandle[] _eventArray;

        public WaitHandle[] EventArray
        {
            get { return _eventArray; }
        }
    }

    class Procedure
    {
        public Procedure(Queue<int> queue, SyncEvents syncEvent)
        {
            this._syncEvents = syncEvent;
            this._queue = queue;
        }

        public void ThreadRun()
        {
            int count = 0;
            Random rand = new Random();

            //waitone(0,false)表示等待线程的释放，并在线程获得信号前不会退出当前同步域
            while (!_syncEvents.ExitThreadEvent.WaitOne(0, false))
            {
                lock (((ICollection)_queue).SyncRoot)
                {
                    while (_queue.Count < 20)
                    {
                        _queue.Enqueue(rand.Next(0, 100));
                        _syncEvents.NewItemEvent.Set();
                        count++;
                    }
                }
            }
            Console.WriteLine("Producer thread: produced {0} items", count);
        }
        private SyncEvents _syncEvents;
        private Queue<int> _queue;
    }

    class Consumer
    {
        public Consumer(Queue<int> queue, SyncEvents syncEvent)
        {
            _queue = queue;
            _syncEvent = syncEvent;
        }
        public void ThreadRun()
        {
            int count = 0;
            while (WaitHandle.WaitAny(_syncEvent.EventArray) != 1)
            {
                lock (((ICollection)_queue).SyncRoot)
                {
                    _queue.Dequeue();
                }
                count++;
            }
            Console.WriteLine("Consumer Thread: consumed {0} items", count);
        }
        private Queue<int> _queue;
        private SyncEvents _syncEvent;
    }
}
