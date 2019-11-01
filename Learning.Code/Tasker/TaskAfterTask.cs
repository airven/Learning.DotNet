using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.Tasker
{
    class TaskAfterTask : ITask
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
            var cts = new CancellationTokenSource();
            var tf = new TaskFactory(cts.Token, TaskCreationOptions.None, TaskContinuationOptions.ExecuteSynchronously, TaskScheduler.Default);
            var childrenTasks = new[]{
                                                    tf.StartNew<string>(() =>
                                                    {
                                                           return "a";

                                                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default),
                                                    tf.StartNew<string>(()=>{
                                                       return "b";
                                                    }, CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default)
                                                    };
            tf.ContinueWhenAll(childrenTasks, completedTasks => completedTasks.Where(t => t.IsCompleted).First(), CancellationToken.None).ContinueWith(
                    async t =>
                    {
                        try
                        {
                            await Task.Delay(1000);
                            Console.WriteLine(t.Result.Result.ToString());
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                    }, TaskContinuationOptions.ExecuteSynchronously);
        }
    }
}
