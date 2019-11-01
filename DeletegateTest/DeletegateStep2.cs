using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest
{
    /*用来多播委托的示例代码*/
    /*该示例演示的一个购物过程,货物可以多买也可以减掉*/
    public class DeletegateStep2
    {
        public delegate void OrderDelegate();
        public void Show()
        {
            OrderDelegate startDelegate = Order.OrderFood;
            startDelegate += Order.OrderFlower;
            startDelegate += Order.OrderDrink;
            startDelegate();

            Console.WriteLine("东西买多了，把鲜花退掉吧");
            startDelegate -= Order.OrderFlower;
            startDelegate();
        }
    }
    public class Order
    {
        public static void OrderFood()
        {
            Console.WriteLine("购买主食");
        }

        public static void OrderFlower()
        {
            Console.WriteLine("购买鲜花");
        }

        public static void OrderDrink()
        {
            Console.WriteLine("购买饮料");
        }
    }
}
