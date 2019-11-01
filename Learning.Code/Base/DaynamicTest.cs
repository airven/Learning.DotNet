using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code.Base
{
    class DaynamicTest : ITask
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
            dynamic person = new ExpandoObject();
            person.Name = "John";
            person.Surname = "Doe";
            person.Age = 42;

            Console.WriteLine($"{person.Name} {person.Surname}, {person.Age}");

            person.ToString = (Func<string>)(() => $"{person.Name} {person.Surname}, {person.Age}");
            Console.WriteLine(person.ToString());


            var dictionary = (IDictionary<string, object>)person;
            foreach (var member in dictionary)
            {
                Console.WriteLine($"{member.Key} = {member.Value}");
            }

            dictionary.Remove("ToString");
            foreach (var member in dictionary)
            {
                Console.WriteLine($"{member.Key} = {member.Value}");
            }
        }
    }
}
