using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
namespace BlockCollectionTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //使用3个生产者，5个消费者来处理数据
            AddTryTake(3, 5);

            //使用3个生产者，5个消费者来处理数据
            BlockCollectionAction(3, 5);

            //使用dataflow模式来处理数据
            DataflowAction();
            Console.Read();
        }

        //使用TryTake来遍历blockcollection
        static Action<int, int> AddTryTake = (producerThreads, consumerThreads) =>
        {
            var stack = new BlockingCollection<int>();
            var startEvent = new ManualResetEventSlim(false);
            var finished = 0;
            var fcc = 0;
            var stop = false;

            var producerTasks = Enumerable.Range(0, producerThreads).Select(i => Task.Factory.StartNew(() =>
            {
                var count = 1000 / producerThreads;
                startEvent.Wait();
                //for (var j = 0; j < count; j++)
                stack.Add(i);
                Interlocked.Increment(ref finished);
                if (finished >= producerThreads) stop = true;
            }, TaskCreationOptions.LongRunning)).ToArray();

            var consumerTasks = Enumerable.Range(0, consumerThreads).Select(i => Task.Factory.StartNew(() =>
            {
                int num;
                startEvent.Wait();
                while (!stop)
                {

                    if (stack.TryTake(out num))
                    {
                        Console.WriteLine(num);
                        Interlocked.Increment(ref fcc);
                    }
                }
            }, TaskCreationOptions.LongRunning)).ToArray();

            //var stopwatch = Stopwatch.StartNew();
            startEvent.Set();
            //stop = true;
            Task.WaitAll(producerTasks);
            Task.WaitAll(consumerTasks);
            Console.WriteLine("次数:" + finished);
            Console.WriteLine("次数x:" + fcc);
            Console.WriteLine("扫描结束");

        };

        //使用GetConsumingEnumerable来遍历blockcollection
        static Action<int, int> BlockCollectionAction = (producerThreads, consumerThreads) =>
        {
            int iterations = 1000;
            var finished = 0;
            var fcc = 0;
            var stack = new BlockingCollection<int>();
            var startEvent = new ManualResetEventSlim(false);
            var stop = false;

            var producerTasks = Enumerable.Range(0, producerThreads).Select(i => Task.Factory.StartNew(() =>
            {
                //var count = iterations / producerThreads;
                startEvent.Wait();
                //for (var j = 0; j < 100; j++)
                stack.Add(i);
                Interlocked.Increment(ref finished);
                if (finished >= producerThreads)
                    stop = true;
            }, TaskCreationOptions.LongRunning)).ToArray();


            var consumertask = Enumerable.Range(0, 100).Select(i => Task.Factory.StartNew(() =>
            {
                //var count = iterations / producerThreads;
                startEvent.Wait();
                foreach (var item in stack.GetConsumingEnumerable())
                {
                    Console.WriteLine(item);
                    Interlocked.Increment(ref fcc);
                }
            }, TaskCreationOptions.LongRunning)).ToArray();
            var consumerTask = Task.Factory.StartNew(() =>
            {

            });
            startEvent.Set();

            Task.WaitAll(producerTasks);
            stack.CompleteAdding();
            Task.WaitAll(consumertask);

            Console.WriteLine("消费次数:" + fcc);
            Console.WriteLine("扫描结束");
        };

        //使用BatchBlock，ActionBlock来演示dataflow数据流模型
        static Action DataflowAction = () =>
        {
            var _batchBlock = new BatchBlock<int>(10, new GroupingDataflowBlockOptions() { MaxNumberOfGroups = -1 });
            var _actionBlock = new ActionBlock<int[]>(p =>
            {
                Thread.Sleep(100);
                Console.WriteLine(string.Join(",", p));
            }, new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 1 });
            _batchBlock.LinkTo(_actionBlock);


            Parallel.ForEach(Enumerable.Range(0, 100), number =>
            {
                _batchBlock.Post(number);
            });

            _batchBlock.Completion.ContinueWith(p => _actionBlock.Complete());
            _batchBlock.Complete();
            _batchBlock.Completion.Wait();
        };
    }
}
