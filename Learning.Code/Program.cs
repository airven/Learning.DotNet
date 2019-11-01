using System;
using System.Collections.Generic;
using System.Reflection;

namespace Learning.Code
{
    class Program
    {
        static void Main()
        {
            try
            {
                Assembly asm = Assembly.GetExecutingAssembly();

                Type[] types = asm.GetTypes();

                IDictionary<int, Type> typeMap = new Dictionary<int, Type>();
                int counter = 1;

                Console.WriteLine("Select example to run: ");
                List<Type> typeList = new List<Type>();
                foreach (Type t in types)
                {
                    if (new List<Type>(t.GetInterfaces()).Contains(typeof(ITask)))
                    {
                        typeList.Add(t);
                    }
                }

                // sort for easier readability
                typeList.Sort(new TypeNameComparer());

                foreach (Type t in typeList)
                {
                    string counterString = string.Format("[{0}]", counter).PadRight(4);
                    Console.WriteLine("{0} {1} {2}", counterString, t.Namespace.Substring(t.Namespace.LastIndexOf(".") + 1), t.Name);
                    typeMap.Add(counter++, t);
                }

                Console.WriteLine();
                Console.Write("> ");
                int num = Convert.ToInt32(Console.ReadLine());
                Type eType = typeMap[num];
                ITask example = MyUtil.InstantiateType<ITask>(eType);
                example.Run();
                Console.WriteLine("Example run successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error running example: " + ex.Message);
                Console.WriteLine(ex.ToString());

            }
            Console.Read();
        }
    }

    public class MyUtil
    {
        public static T InstantiateType<T>(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type", "Cannot instantiate null");
            }
            ConstructorInfo ci = type.GetConstructor(Type.EmptyTypes);
            if (ci == null)
            {
                throw new ArgumentException("Cannot instantiate type which has no empty constructor", type.Name);
            }
            return (T)ci.Invoke(new object[0]);
        }
    }
    public class TypeNameComparer : IComparer<Type>
    {
        public int Compare(Type t1, Type t2)
        {
            if (t1.Namespace.Length > t2.Namespace.Length)
            {
                return 1;
            }

            if (t1.Namespace.Length < t2.Namespace.Length)
            {
                return -1;
            }

            return t1.Namespace.CompareTo(t2.Namespace);
        }
    }
}
