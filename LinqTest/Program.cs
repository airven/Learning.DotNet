using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqComparerExample example1 = new LinqComparerExample();
            example1.Run();

            LinqExpressTreeExample example2 = new LinqExpressTreeExample();
            example2.Run();
        }
    }
}
