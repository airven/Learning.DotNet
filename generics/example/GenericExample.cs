using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics.example
{
    class GenericExample : ITask
    {
        public void Show()
        {
            //IBaseDAL baseDAL = new BaseDAL();
            IBaseDAL baseDAL = new BaseDALV2();

            // 插入
            Student studentIns = new Student()
            {
                Name = "小明",
                Age = 20,
                Sex = 2,
                Email = "xiaoming@qq.com"
            };
            bool resultIns = baseDAL.Insert<Student>(studentIns) > 0 ? true : false;
            Console.WriteLine($"插入执行结果：{resultIns}");

            //查询单条记录
            var student = baseDAL.Query<Student>(4);
            Console.WriteLine($"{student.Name}");

            //查询多条记录
            var studentList = baseDAL.QueryAll<Student>();
            foreach (var item in studentList)
            {
                Console.WriteLine($"{item.Name}");
            }

            // 更新
            Student studentUpd = new Student()
            {
                Id = 1,
                Name = "zhangsan1234",
                Age = 20,
                Sex = 2,
                Email = "zhangsan1234@qq.com"
            };
            bool resultUpd = baseDAL.Update(studentUpd) > 0 ? true : false;
            Console.WriteLine($"更新执行结果：{resultUpd}");

            bool resultDel = baseDAL.Delete<Student>(3) > 0 ? true : false;
            Console.WriteLine($"删除执行结果：{resultDel}");

            Console.Read();
        }
    }
}
