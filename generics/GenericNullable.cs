using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics
{
    //System.Nullable<T>的使用 int ?  =nullable<int>
    class GenericNullable : ITask
    {
        string GetInfo(int ? Age, string Name)
        {
            if(Age.HasValue)
                return $"姓名：{Name},年龄：{Age}";
            return "年龄不能为0";
        }
        public void Show()
        {
            string personInfo = GetInfo(null, "jack");
            Console.WriteLine(personInfo);

            string personInfo2 = GetInfo(13, "geek");
            Console.WriteLine(personInfo2);
        }
    }
}
