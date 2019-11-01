using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRaft
{
    /// <summary>
    /// 模仿raft协议，将数据进行分片
    /// </summary>
    public class Server
    {
        public int Port { get; set; }
        public string Ip { get; set; }

        public string DbType { get; set; }
    }
    public class Program
    {
        private static readonly SortedDictionary<ulong, string> _circle = new SortedDictionary<ulong, string>();
        static void Main(string[] args)
        {
            //演示一致性hash算法
            PipleData();
            Console.Read();
        }
        static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        public static void PipleData()
        {
            int Replicas = 200;

            AddNode("10.16.0.1", Replicas);
            AddNode("10.16.0.2", Replicas);
            AddNode("10.16.0.3", Replicas);

            List<string> nodes = new List<string>();
            for (int i = 0; i < 5000; i++)
            {
                nodes.Add(GetTargetNode(i + "user" + (char)i));
            }
            var counts = nodes.GroupBy(n => n, n => n.Count()).ToList();
            counts.ForEach(index => Console.WriteLine(index.Key + "-" + index.Count()));
        }
        public static void AddNode(string node, int repeat)
        {
            for (int i = 0; i < repeat; i++)
            {
                string identifier = node.GetHashCode().ToString() + "-" + i;
                ulong hashCode = Md5Hash(identifier);
                _circle.Add(hashCode, node);
            }
        }
        public static ulong Md5Hash(string key)
        {
            using (var hash = System.Security.Cryptography.MD5.Create())
            {
                byte[] data = hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                var a = BitConverter.ToUInt64(data, 0);
                var b = BitConverter.ToUInt64(data, 8);
                ulong hashCode = a ^ b;
                return hashCode;
            }
        }
        public static string GetTargetNode(string key)
        {
            ulong hash = Md5Hash(key);
            ulong firstNode = ModifiedBinarySearch(_circle.Keys.ToArray(), hash);
            return _circle[firstNode];
        }

        /// <summary>
        /// 计算key的数值，得出空间归属。
        /// </summary>
        /// <param name="sortedArray"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static ulong ModifiedBinarySearch(ulong[] sortedArray, ulong val)
        {
            int min = 0;
            int max = sortedArray.Length - 1;

            if (val < sortedArray[min] || val > sortedArray[max])
                return sortedArray[0];

            while (max - min > 1)
            {
                int mid = (max + min) / 2;
                if (sortedArray[mid] >= val)
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }
            return sortedArray[max];
        }
    }
}
