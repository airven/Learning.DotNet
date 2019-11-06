using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceTest
{
    class abstractTest : ITask
    {
        public void Show()
        {
            Major major1 = new Undergraduate();
            major1.Id = 1;
            major1.Name = "张晓";
            Console.WriteLine("本科生信息：");
            Console.WriteLine("学号：" + major1.Id + "姓名：" + major1.Name);
            major1.Requirement();
            Major major2 = new Graduate();
            major2.Id = 2;
            major2.Name = "李明";
            Console.WriteLine("研究生信息：");
            Console.WriteLine("学号：" + major2.Id + "姓名：" + major2.Name);
            major2.Requirement();
        }
    }
    /* 总而言之，使用继承实现多态必须满足以下两个条件。
   子类在继承父类时必须有重写的父类的方法。
   在调用重写的方法时，必须创建父类的对象指向子类(即子类转换成父类)*/
    abstract class Major
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public abstract void Requirement();
    }
    class Undergraduate : Major
    {
        public override void Requirement()
        {
            Console.WriteLine("本科生学制4年，必须修满48学分");
        }
    }
    class Graduate : Major
    {
        public override void Requirement()
        {
            Console.WriteLine("研究生学制3年，必须修满32学分");
        }
    }
}
