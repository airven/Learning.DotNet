using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqTest
{
    public class LinqExpressTreeExample 
    {
        public void Run()
        {
            //以下是常见的运算符
            //GreaterThan
            //GreaterThanOrEqual
            //Equal
            //LessThan
            //LessThanOrEqual
            //GreaterThanOrEqual
            //LessThanOrEqual
            //GreaterThanOrEqual
            //Equal
            //Equal
            //Equal
            //LessThan
            //GreaterThan
            //GreaterThanOrEqual
            //Equal


            IList<RuleModule> ruleModuleList = new List<RuleModule>() {
                new RuleModule {MemberName="CPU",TargetValue="30",Operator="GreaterThan" },
                new RuleModule {MemberName="Connection",TargetValue="15", Operator="GreaterThan" }
                //new RuleModule {MemberName="CPU",TargetValue="10",Operator="GreaterThan" },
                //new RuleModule {MemberName="CPU",TargetValue="25", Operator="LessThanOrEqual" },
                //new RuleModule {MemberName="Block",TargetValue="20", Operator="GreaterThanOrEqual" },
                //new RuleModule {MemberName="Warntime",TargetValue="2015022415", Operator="GreaterThan" }
            };

            IList<computer> computerinfolist = new List<computer>() {
                new computer{ CPU = 30, Connection= 5, Block=12, Warntime =201524614},
                new computer{ CPU = 500, Connection=15, Block=1, Warntime =20146154},
                new computer{ CPU = 20, Connection= 20, Block=22, Warntime =2015254},
                new computer{ CPU = 520, Connection= 52, Block=52, Warntime =20146154},
            };

            var a = computerinfolist.Where(RuleEngine.CompileMultiRule<computer>(ruleModuleList));
            foreach(var item in a)
            {
                Console.WriteLine($"电脑性能cpu:{item.CPU},连接数:{item.Connection},阻塞数{item.Block},时间点:{item.Warntime}");
            }

        }
    }
    public class computer
    {
        public int CPU { get; set; }
        public int Connection { get; set; }
        public int Block { get; set; }
        public int Warntime { get; set; }
    }
}
