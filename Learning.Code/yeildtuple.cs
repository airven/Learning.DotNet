using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code
{
    class yeildtuple : ITask
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
            var a =ProduceIndices();
            a.ForAll(n => { Console.WriteLine($"{n.Item1},{n.Item2}"); });
          
        }

        private static IEnumerable<Tuple<int, int>> ProduceIndices()
        {
            for (int x = 0; x < 100; x++)
                for (int y = 0; y < 100; y++)
                    yield return Tuple.Create(x, y);
        }

        private static IEnumerable<Tuple<int, int>> QueryIndices()
        {
            return from x in Enumerable.Range(0, 100)
                   from y in Enumerable.Range(0, 100)
                   select Tuple.Create(x, y);
        }
    }

    static class Extensions
    {
        public static void ForAll<T>(
    this IEnumerable<T> sequence,
    Action<T> action)
        {
            foreach (T item in sequence)
                action(item);
        }
    }
}
