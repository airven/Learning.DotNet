using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest
{
    /*事件,事件可以理解为一种特殊的委托*/
    
    public class DeletegateStep3
    {
        public delegate void SayDelegate();
        public event SayDelegate sayEvent;

        public void EventTrigger()
        {
            if (sayEvent != null)
                sayEvent();
        }

        public void PrintFrist()
        {
            Console.WriteLine("event triggered !");
        }
        public void Show1()
        {
            sayEvent = new SayDelegate(PrintFrist);
            EventTrigger();
        }

        public void Show2()
        {
            MyEvent myEvent = new MyEvent();
            myEvent.sayEvent += new MyEvent.SayDelegate(MyEvent.OrderDrink);
            myEvent.sayEvent += new MyEvent.SayDelegate(MyEvent.OrderFlower);
            myEvent.sayEvent += new MyEvent.SayDelegate(MyEvent.OrderFood);
            myEvent.InvokeEvent();
        }
    }

    public class MyEvent
    {
        public delegate void SayDelegate();
        public event SayDelegate sayEvent;

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

        public void InvokeEvent()
        {
            if (sayEvent != null)
                sayEvent();
        }
    }

}
