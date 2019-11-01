using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.Tasker
{
    public class TaskAndManualResetEvent : ITask
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
            var manualResetEventSlim = new ManualResetEventSlim(false);
            var task = System.Threading.Tasks.Task.Run(async () =>
            {
                try
                {
                    /*处理并行的代码逻辑*/
                    await System.Threading.Tasks.Task.Delay(1000);
                }
                finally
                {
                    manualResetEventSlim.Set();
                }
            });
            manualResetEventSlim.Wait();
            task.Wait();
        }
    }
}
