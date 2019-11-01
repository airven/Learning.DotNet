using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ParallelTest
{
    class Program
    {
        static object a = new object();
        public static void Main()
        {
            //演示并行中如何共享对象localvarible，从而实例化开销
            long total = 0;
            Parallel.ForEach(Enumerable.Range(0, 101),
                () => 0,
                (element, loopstate, localStorage) =>
                {
                    localStorage += element;
                    return localStorage;
                },
                (localTotal) => { total += localTotal; });
            Console.WriteLine(total);
            Console.WriteLine("Hello World");


            ConcurrentQueue<int> inlist = new ConcurrentQueue<int>();
            Parallel.ForEach(Enumerable.Range(0, 20), (element, loopstate, localStorage) =>
            {
                inlist.Enqueue(element);
            });
            foreach (var a in inlist)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("Hello World");


            //并分分区使用
            Stopwatch watch = new Stopwatch();
            watch.Start();
            var result = ParallelForWithLocalFinally();
            Console.WriteLine(result);

            watch.Stop();
            Console.WriteLine(string.Format("Total execution time in TotalMilliseconds is {0} ", watch.Elapsed.TotalMilliseconds));


            //演示task.wait(timeout)，基于CancellationTokenSource的任务取消
            var TokenSource = new CancellationTokenSource();
            var token = TokenSource.Token;
            var task = Task<int>.Run(() =>
            {
                Thread.Sleep(5000);
                //http request
                return 1;
                if (!token.IsCancellationRequested)
                {
                    Console.WriteLine("已完成");
                }
                token.ThrowIfCancellationRequested();
            }, token);

            if (!task.Wait(3000, TokenSource.Token))
            {
                TokenSource.Cancel();
                Console.WriteLine("已取消");
            }
            try
            {
                Console.WriteLine(task.Result);
            }
            catch (AggregateException ex)
            {
                ex.Handle(e => e is OperationCanceledException);
                Console.WriteLine("sum已经被取消");
            }



            //测试task.ContinueWith的使用
            var serverBlockList = new BlockingCollection<int>();
            Action action = async () =>
            {
                await Insert(serverBlockList);
            };
            action();




            //测试 ManualResetEventSlim的使用场景
            var manualResetEventSlim = new ManualResetEventSlim(false);
            var tasks = Enumerable.Range(0, 100).Select((host) => Task.Run(() =>
            {
                try
                {
                    manualResetEventSlim.Wait();
                    Console.WriteLine($"获取alwayson主从信息失败{host}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"获取alwayson主从信息失败{ex.Message}");
                }
            }));
            try
            {
                manualResetEventSlim.Set();
                Task.WhenAll(tasks);
                Console.WriteLine("累计数量{0}", serverBlockList.Count);
                serverBlockList.CompleteAdding();
            }
            catch (AggregateException exception)
            {
                foreach (var ex in exception.InnerExceptions)
                {
                    Console.WriteLine($"{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
            finally
            {
                manualResetEventSlim.Dispose();
            }
            Console.Read();
        }
        private static int ITEMS { get; set; } = 100;

        private static int[] arr = null;
        public static long ParallelForWithLocalFinally()
        {
            long total = 0;
            int parts = 100;
            int partSize = ITEMS / parts;
            arr = new int[ITEMS];
            for (int i = 1; i < ITEMS + 1; i++)
            {
                arr[i - 1] = i;
            }
            var parallel = Parallel.For(0, parts + 1,
                localInit: () => 0L, // Initializes the "localTotal"
                body: (iter, state, localTotal) =>
                {
                        //for (int j = iter * partSize; j < (iter + 1) * partSize; j++)
                        //{
                        //    localTotal += arr[j];
                        //    //Interlocked.Add(ref total, arr[j]);
                        //}
                        return localTotal = localTotal + iter;
                },
                localFinally: (localTotal) => { total += localTotal; });
            return total;
        }

        static async Task Insert(BlockingCollection<int> bccollection)
        {
            await Task.Factory.StartNew<string>(() =>
            {
                Console.WriteLine("hello,welcome!");
                return "hello,welcome";
            }).ContinueWith((async compelers =>
            {
                if (compelers.IsCompleted)
                {
                    var task1 = Enumerable.Range(0, 10).Select((i) => Task.Run(() =>
                    {
                        bccollection.Add(i);
                    }));
                    await Task.WhenAll(task1);
                    bccollection.CompleteAdding();
                }
            }), CancellationToken.None);
        }
    }
}
