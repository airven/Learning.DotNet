using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning.Code.Base
{
    class ForExample : ITask
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
            //for(var i=0;i<10;i++)
            //{
            //    if (i == 5)
            //        continue;
            //    else
            //        Console.WriteLine(i);
            //}
            //Random ran=new Random();
            ////int RandKey = ran.Next(1, 100);
            //for (var a=0;a<100; a++ )
            //{
            //    int RandKey = ran.Next(1, 100);
            //    Console.WriteLine(RandKey);
            //}
            //var userlist = new List<User> {
            //    new User {Age=1,UserName="账号" },
            //    new User {Age=2,UserName="fjksdjfksdj" }
            //};

            //foreach(var item in userlist)
            //{
            //    item.Age = 2;
            //    item.UserName = "jfkdjf";
            //}
            //foreach(var a in userlist)
            //{
            //    Console.WriteLine(a.UserName + "___" + a.Age);
            //}
            
            var a = Convert.ToBase64String(new ASCIIEncoding().GetBytes("02723:gLJNRzjt"));
            Console.WriteLine(a);
        }
    }

    public class User
    {
        public string UserName { get; set; }
        public int Age { get; set; }
    }
}
