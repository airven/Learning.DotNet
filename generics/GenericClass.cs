using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics
{
    public class Animal
    {
        public string Feature { get; set; }
        public string Name { get; set; }
    }

    public class Cat : Animal
    {
    }

    /// <summary>
    /// 定义一个泛型类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NodeList<T>
    {
        private List<T> nodes;
        public NodeList()
        {
            this.nodes = new List<T>();
        }
        public void AddNode(T t)
        {
            this.nodes.Add(t);
        }
        public void RmoveNode(T t)
        {
            this.nodes.Remove(t);
        }
        public void ProcessAllNodes()
        {
            foreach (var node in nodes)
            {
                Console.WriteLine(node.ToString());
            }
        }
    }

    /// <summary>
    /// 定义一个泛型接口,演示逆变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomListInA<in T>
    {
        void Print(T t);
    }

    /// <summary>
    /// 定义一个泛型接口,演示协变
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomListInB<out T>
    {
        T Find();
    }

    public class CustomListInA<T> : ICustomListInA<T>
    {
        public void Print(T t)
        {
            Console.WriteLine(typeof(T).FullName);
        }
    }
    public class CustomListInB<T> : ICustomListInB<T> where T : new()
    {
        public T Find()
        {
            return new T();
        }
    }

    //无参返回值的泛型委托
    public delegate void FeedDelegateA<in T>(T target);

    //有参返回值的泛型委托
    public delegate T FeedDelegateB<out T>();


    //这是错误的，普通类是不能用in out来修饰的
    //public class Tool<in T>
    //{
    //}
    public class GenericClass : ITask
    {
        public void Show()
        {
            NodeList<string> list = new NodeList<string>();
            list.AddNode("a");
            list.AddNode("b");
            list.AddNode("c");
            list.ProcessAllNodes();

            ICustomListInA<Cat> customLstInA = new CustomListInA<Animal>();
            Cat animal = new Cat();
            customLstInA.Print(animal);

            ICustomListInB<Animal> customLstInB = new CustomListInB<Cat>();
            Cat animalx = new Cat();
            customLstInB.Find();


            FeedDelegateA<Animal> feedAnimalMethod = a => Console.WriteLine($"{a.Name} and fature is {a.Feature}");
            Animal an = new Animal
            {
                Name = "cat jack",
                Feature = "cat is running"
            };
            feedAnimalMethod(an);

            Cat cat = new Cat
            {
                Name = "cat cata",
                Feature = "cat is running too"
            };
            feedAnimalMethod(cat);

            FeedDelegateA<Cat> feedDogMethod = feedAnimalMethod;
            feedDogMethod(cat);


            FeedDelegateB<Cat> findCat = () => new Cat
            {
                Name = "cat cataa",
                Feature = "cata is running too"
            };
            FeedDelegateB<Animal> findAnimal = findCat;
            var animalA = findAnimal();
            Console.WriteLine($"{animalA.Name} and {animalA.Feature}");
        }
    }
}
