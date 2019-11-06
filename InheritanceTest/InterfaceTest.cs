using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InheritanceTest
{
    /*
     * 定义接口并使用类实现了接口中的成员。
     *创建接口的实例指向不同的实现类对象。
     */
    class InterfaceTest : ITask
    {
        public void Show()
        {
            IShape shape1 = new Rectangle(10, 20);
            shape1.X = 100;
            shape1.Y = 200;
            shape1.Color = "红色";
            shape1.Draw();
            IShape shape2 = new Circle(10);
            shape2.X = 300;
            shape2.Y = 500;
            shape2.Color = "蓝色";
            shape2.Draw();
        }
    }

    interface IShape
    {
        double Area { get; }
        double X { get; set; }
        double Y { get; set; }
        string Color { get; set; }
        void Draw();
    }
    class Rectangle : IShape
    {
        public Rectangle(double length, double width)
        {
            this.Length = length;
            this.Width = width;
        }
        public double Length { get; set; }//定义长方形的长度
        public double Width { get; set; }//定义长方形的宽度
        public double Area
        {
            get
            {
                return Length * Width;
            }
        }
        public double X { get; set; }
        public double Y { get; set; }
        public string Color { get; set; }
        public void Draw()
        {
            Console.WriteLine("绘制图形如下：");
            Console.WriteLine("在坐标 {0},{1} 的位置绘制面积为 {2} 颜色为 {3} 的矩形", X, Y, Area, Color);
        }
    }
    class Circle : IShape
    {
        //为圆的半径赋值
        public Circle(double radius)
        {
            this.Radius = radius;
        }
        public double Radius { get; set; }
        public double Area
        {
            get
            {
                return Radius * Radius * 3.14;
            }
        }
        public string Color { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public void Draw()
        {
            Console.WriteLine("绘制图形如下：");
            Console.WriteLine("在坐标为 {0},{1} 的位置绘制面积为 {2} 颜色为 {3} 的圆形", X, Y, Area, Color);
        }
    }
}
