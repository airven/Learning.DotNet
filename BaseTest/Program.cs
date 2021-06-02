using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 9;
            Console.WriteLine(GetValue(i));
        }
        static int GetValue(int a)
        {
            try
            {
                a = 3;
                return a;

            }
            finally
            {
                a = 0;
            }
            return a;
        }
    }
}
