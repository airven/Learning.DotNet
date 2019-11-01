using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    public class TcpClientExample : ITask
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
            using (TcpClient tcp = new TcpClient())
            {
                IAsyncResult ar = tcp.BeginConnect("10.100.100.100", 10000, null, null);

                System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                try
                {
                    if (ar.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(10), false))
                    {
                        var b = 4;
                        //tcp.Close();
                        //throw new TimeoutException();
                    }
                    else
                    {
                        var a=10;
                    }
                    tcp.EndConnect(ar);
                }
                catch (Exception ex)
                {
                    throw new TimeoutException();
                }
                finally
                {
                    wh.Close();
                }

                test();
                Console.Read();
            }
        }

        int test()
        {
            try
            {
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
            finally
            {
                var a = 1;
            }
        }
    }
}
