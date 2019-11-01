using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Learning.Code.ThreadTest
{
    class WaitHandleExample : ITask
    {
        public string Name
        {
            get
            {
                return GetType().Name;
            }
        }

        public void Run()
        {

            Calculate cal = new Calculate();
            Console.WriteLine("Result = {0}.", cal.Result(5).ToString());
        }
    }


    public class Calculate
    {
        double baseNumber, firstItem, secondItem, thirdItem;
        AutoResetEvent[] auEvent;
        ManualResetEvent manualEvent;
        Random randgenerate;

        public Calculate()
        {
            auEvent = new AutoResetEvent[]{
                new AutoResetEvent(false),
                new AutoResetEvent(false),
                new AutoResetEvent(false)
            };
            manualEvent = new ManualResetEvent(false);
        }

        void CalculateBase(object stateInfo)
        {
            baseNumber = randgenerate.Next();
            manualEvent.Set();
        }

        void CalculateFirstItem(object stateInfo)
        {
            double preCalc = randgenerate.Next();
            manualEvent.WaitOne();

            firstItem = preCalc * baseNumber * randgenerate.Next();
            auEvent[0].Set();
        }

        void CalculateSecondItem(object stateInfo)
        {
            double preCalc = randgenerate.Next();
            manualEvent.WaitOne();

            secondItem = preCalc * baseNumber * randgenerate.Next();
            auEvent[1].Set();
        }

        void CalculateThirdItem(object stateInfo)
        {
            double preCalc = randgenerate.Next();
            manualEvent.WaitOne();

            thirdItem = preCalc * baseNumber * randgenerate.Next();
            auEvent[2].Set();
        }

        public double Result(int seed)
        {
            randgenerate = new Random(seed);

            // Simultaneously calculate the terms.
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(CalculateBase));
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(CalculateFirstItem));
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(CalculateSecondItem));
            ThreadPool.QueueUserWorkItem(
                new WaitCallback(CalculateThirdItem));

            // Wait for all of the terms to be calculated.
            WaitHandle.WaitAll(auEvent);

            Console.WriteLine("-------1-------");

            Console.WriteLine(firstItem);
            Console.WriteLine(secondItem);
            Console.WriteLine(thirdItem);

            // Reset the wait handle for the next calculation.
            manualEvent.Reset();

            Console.WriteLine("-------2-------");
            Console.WriteLine(firstItem);
            Console.WriteLine(secondItem);
            Console.WriteLine(thirdItem);

            Console.WriteLine("-------3-------");
            return firstItem + secondItem + thirdItem;
        }
    }
}
