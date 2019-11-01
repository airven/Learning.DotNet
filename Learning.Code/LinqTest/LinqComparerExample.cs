using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code.LinqTest
{
    public class LinqComparerExample : ITask
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
            var a = new List<Student>{
                      new Student { Age=12,StuName="abc"},
                      new Student { Age=13,StuName="bc"},
                      new Student { Age=14,StuName="fsdfsdfsd"}
            };

            var b = new List<Student>{
                      new Student { Age=14,StuName="abc"},
                      new Student { Age=15,StuName="bc"},
                      new Student { Age=16,StuName="bcd"}
            };

            foreach (var item in a)
            {
                //方法一
                if (b.Contains(item, new MyIEqualityComparer<Student>(delegate (Student x, Student y)
                {
                    if (null == x || null == y)
                        return false;
                    return x.StuName == y.StuName;
                }, delegate (Student info)
                    {
                        return info.ToString().GetHashCode();
                    }))){
                    Console.WriteLine(item.StuName);
                }

                
                Console.WriteLine("#------分隔线-------#");
                //方法二
                //if (b.Contains(item, Equality<Student>.CreateComparer(p => p.StuName)))
                //{
                //    Console.WriteLine(item.StuName);
                //}
            }

            Console.WriteLine("#-------------#");
            Console.WriteLine("calculate end");
        }
    }

    public class Student
    {
        public string StuName { get; set; }
        public int Age { get; set; }

    }
    public static class Equality<T>
    {
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return new CommonEqualityComparer<V>(keySelector);
        }
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new CommonEqualityComparer<V>(keySelector, comparer);
        }

        class CommonEqualityComparer<V> : IEqualityComparer<T>
        {
            private Func<T, V> keySelector;
            private IEqualityComparer<V> comparer;

            public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }
            public CommonEqualityComparer(Func<T, V> keySelector)
                : this(keySelector, EqualityComparer<V>.Default)
            { }

            public bool Equals(T x, T y)
            {
                return comparer.Equals(keySelector(x), keySelector(y));
            }
            public int GetHashCode(T obj)
            {
                return comparer.GetHashCode(keySelector(obj));
            }
        }
    }


    /// <summary>
    /// A class to wrap the IEqualityComparer interface into matching functions for simple implementation
    /// </summary>
    /// <typeparam name="T">The type of object to be compared</typeparam>
    public class MyIEqualityComparer<T> : IEqualityComparer<T>
    {
        /// <summary>
        /// Create a new comparer based on the given Equals and GetHashCode methods
        /// </summary>
        /// <param name="equals">The method to compute equals of two T instances</param>
        /// <param name="getHashCode">The method to compute a hashcode for a T instance</param>
        public MyIEqualityComparer(Func<T, T, bool> equals, Func<T, int> getHashCode)
        {
            if (equals == null)
                throw new ArgumentNullException("equals", "Equals parameter is required for all MyIEqualityComparer instances");
            EqualsMethod = equals;
            GetHashCodeMethod = getHashCode;
        }
        /// <summary>
        /// Gets the method used to compute equals
        /// </summary>
        public Func<T, T, bool> EqualsMethod { get; private set; }
        /// <summary>
        /// Gets the method used to compute a hash code
        /// </summary>
        public Func<T, int> GetHashCodeMethod { get; private set; }

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            return EqualsMethod(x, y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            if (GetHashCodeMethod == null)
                return obj.GetHashCode();
            return GetHashCodeMethod(obj);
        }
    }
}
