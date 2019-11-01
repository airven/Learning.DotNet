using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Security.Cryptography;

namespace Learning.Util
{
    public class HashCompute
    {
        static  void Test(string[] args)
        {
            KetamaNodeLocator nodelocator = new KetamaNodeLocator(new List<string> {"1","2","3"},3);
            Console.WriteLine(nodelocator.GetPrimary("10.16.0.2"));
            Console.WriteLine(nodelocator.GetPrimary("10.16.0.4"));
            Console.WriteLine(nodelocator.GetPrimary("10.16.0.6"));
            Console.WriteLine(nodelocator.GetPrimary("10.16.0.6"));
            Console.WriteLine(nodelocator.GetPrimary("10.16.0.5"));
        }

        public class DataBaseSchedule
        {
            public bool running { get; set; }
            Thread thread;
            Task task;
            public DataBaseSchedule(Action action, TimeSpan timeout)
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



    public class KetamaNodeLocator
    {
        //原文中的JAVA类TreeMap实现了Comparator方法，这里我图省事，直接用了net下的SortedList，其中Comparer接口方法）
        private SortedList<long, string> ketamaNodes = new SortedList<long, string>();
        private HashAlgorithm hashAlg;
        private int numReps = 160;

        //此处参数与JAVA版中有区别，因为使用的静态方法，所以不再传递HashAlgorithm alg参数
        public KetamaNodeLocator(List<string> nodes, int nodeCopies)
        {
            ketamaNodes = new SortedList<long, string>();

            numReps = nodeCopies;
            //对所有节点，生成nCopies个虚拟结点
            foreach (string node in nodes)
            {
                //每四个虚拟结点为一组
                for (int i = 0; i < numReps /3; i++)
                {
                    //getKeyForNode方法为这组虚拟结点得到惟一名称 
                    byte[] digest = HashAlgorithm.computeMd5(node + i);
                    /** Md5是一个16字节长度的数组，将16字节的数组每四个字节一组，分别对应一个虚拟结点，这就是为什么上面把虚拟结点四个划分一组的原因*/
                    for (int h = 0; h < 3; h++)
                    {
                        long m = HashAlgorithm.hash(digest, h);
                        ketamaNodes[m] = node;
                    }
                }
            }
        }

        public string GetPrimary(string k)
        {
            byte[] digest = HashAlgorithm.computeMd5(k);
            string rv = GetNodeForKey(HashAlgorithm.hash(digest, 0));
            return rv;
        }

        string GetNodeForKey(long hash)
        {
            string rv;
            long key = hash;
            //如果找到这个节点，直接取节点，返回 
            if (!ketamaNodes.ContainsKey(key))
            {
                //得到大于当前key的那个子Map，然后从中取出第一个key，就是大于且离它最近的那个key 
                var tailMap = from coll in ketamaNodes
                              where coll.Key > hash
                              select new { coll.Key };
                if (tailMap == null || tailMap.Count() == 0)
                    key = ketamaNodes.FirstOrDefault().Key;
                else
                    key = tailMap.FirstOrDefault().Key;
            }
            rv = ketamaNodes[key];
            return rv;
        }
    }
    public class HashAlgorithm
    {
        public static long hash(byte[] digest, int nTime)
        {
            long rv = ((long)(digest[3 + nTime * 4] & 0xFF) << 24)
            | ((long)(digest[2 + nTime * 4] & 0xFF) << 16)
            | ((long)(digest[1 + nTime * 4] & 0xFF) << 8)
            | ((long)digest[0 + nTime * 4] & 0xFF);

            return rv & 0xffffffffL; /* Truncate to 32-bits */
        }

        /**
        * Get the md5 of the given key.
*/
        public static byte[] computeMd5(string k)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            byte[] keyBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(k));
            md5.Clear();
            //md5.update(keyBytes);
            //return md5.digest();
            return keyBytes;
        }
    }
}
