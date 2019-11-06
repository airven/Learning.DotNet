using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceTest
{

    class VirtualTest : ITask
    {
        public void Show()
        {
            //B b = new B("ok");
            Person person = new Student(Name: "fjkdj", Address: "fjkdjf", Age: 12, Course: "fjkdjf");
            //Person person = new Student();
            person.Show();
            person.Print();

            Person teacher = new Teacher();
            teacher.Show();
        }
    }


    class Person
    {
        public Person()
        {
            Console.WriteLine("Person类的构造器");
        }
        public Person(string Name, string Address, int Age)
        {
            this.Name = Name;
            this.Address = Address;
            this.Age = Age;
        }
        public string Name { get; set; }
        public int Age { get; set; } = 20;
        public virtual string Address { get; set; }
        public void Print()
        {
            Console.WriteLine("hello,welcome");
        }
        public virtual void Show()
        {
            Console.WriteLine("just so so");
        }
    }

    class Student : Person
    {
        public string Course { get; set; }
        public Student()
        {
            Console.WriteLine("Student类的构造器");
        }
        //在子类的构造器中都会自动调用父类的无参构造器，如果需要调用父类中带参数的构造器才使用“:base(参数)”的形式
        public Student(string Name, string Address, int Age, string Course) : base(Name, Address, Age)
        {
            this.Course = Course;
        }
        public override string Address { get => base.Address; set => base.Address = value; }
        public override void Show()
        {
            base.Show();
            Console.WriteLine($"{ this.Name}-{this.Address }-{this.Course }");
        }
        public void Test() { }
    }

    class Teacher : Person
    {
        //如果在父类中没有无参构造器，必须在子类的构造器中继承父类的构造器，否则程序无法成功编译
        public new void Show()
        {
            Console.WriteLine("my name is jack");
        }
    }
    class A
    {
        public A()
        {
            Console.WriteLine("A类的构造器");
        }
    }
    class B : A
    {
        public B()
        {
            Console.WriteLine("B类的构造器");
        }
        public B(string name)
        {
            Console.WriteLine("B类中带参数的构造器，传入的值为：" + name);
        }
    }
}
