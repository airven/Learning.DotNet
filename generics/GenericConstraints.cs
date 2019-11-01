using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics
{
    class GenericConstraints : ITask
    {
        public T GetInfoA<T>(T t)where T:struct
        {
            return t;
        }
        public T GetInfoB<T>(T t) where T:new ()
        {
            return t;
        }
        public T GetInfoC<T>(T t) where T : class
        {
            return t;
        }
        public T GetInfoD<T,U>(T t,U u) where T : class where U:struct
        {
            return t;
        }
        public T GetInfoE<T, U>(T t, U u) where T : U 
        {
            return t;
        }

        public void Show()
        {
            MyGenericClass<string> generic = new MyGenericClass<string>("hello,welcome");
            var a= generic.genericMethod("hello,jack");
            Console.WriteLine(a);
        }
    }

    class MyGenericClass<T> where T : class
    {
        private T genericMemberVariable;

        public MyGenericClass(T value)
        {
            genericMemberVariable = value;
        }
        public T genericMethod(T genericParameter)
        {
            Console.WriteLine("Parameter type: {0}, value: {1}", typeof(T).ToString(), genericParameter);
            Console.WriteLine("Return type: {0}, value: {1}", typeof(T).ToString(), genericMemberVariable);
            return genericMemberVariable;
        }

        public T genericProperty { get; set; }
    }

}
