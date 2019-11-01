using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeletegateTest
{
    /*委托在重构代码中的作用*/
    /*需求：我有很多类型的数据库,根据传过的不同类型，执行相应的动作*/
    public class DeletegateStep4
    {
        public enum DBType
        {
            SqlServer = 1,
            MySql = 2,
            MongoDB = 3,
            UNKNOW = 0
        }
        //版本一
        public void ShowMessageA(string dbtype)
        {
            switch (dbtype)
            {
                case "SqlServer":
                    Console.WriteLine("this data's dbtype is SqlServer");
                    break;
                case "MySql":
                    Console.WriteLine("this data's dbtype is MySql");
                    break;
                case "MongoDB":
                    Console.WriteLine("this data's dbtype is MongoDB");
                    break;
                default:
                    Console.WriteLine("I don't know dbtype");
                    break;
            }
        }

        //版本二,使用枚举
        private DBType dbType;
        public void ShowMessageB(string Dbtype)
        {
            this.dbType = (DBType)Enum.Parse(typeof(DBType), Dbtype);
            switch (this.dbType)
            {
                case DBType.SqlServer:
                    Console.WriteLine("this data's dbtype is SqlServer");
                    break;
                case DBType.MySql:
                    Console.WriteLine("this data's dbtype is MySql");
                    break;
                case DBType.MongoDB:
                    Console.WriteLine("this data's dbtype is MongoDB");
                    break;
                default:
                    Console.WriteLine("I don't know dbtype");
                    break;
            }
        }

        //版本三，去掉switch结构,使用
        private Dictionary<DBType, Action> dbTypeLogic = new Dictionary<DBType, Action>();
        public void Init()
        {
            dbTypeLogic.Add(DBType.SqlServer, () => { Console.WriteLine("this data's dbtype is SqlServer"); });
            dbTypeLogic.Add(DBType.MongoDB, () => { Console.WriteLine("this data's dbtype is MongoDB"); });
            dbTypeLogic.Add(DBType.MySql, () => { Console.WriteLine("this data's dbtype is MySql"); });
        }
        public void ShowMessageC(string Dbtype)
        {
            this.dbType = (DBType)Enum.Parse(typeof(DBType), Dbtype);
            dbTypeLogic[this.dbType]();
        }

        //版本四,将委托列表中的委托类型，由Action变更为Func，这样将可以利用Func返回值的情况加以丰富
        //Func的返回，将会使得不同的类型的dbtype能触发更多的动作，处理更多的逻辑
        interface IDbTypeLogic
        {
            string GetMessage();
            void GetDetail();
        }
        class MysqlLogic : IDbTypeLogic
        {
            public void GetDetail()
            {
                Console.WriteLine("this is mysql dbInstance");
            }

            public string GetMessage()
            {
                return "mysql";
            }
        }
        class SqlServerLogic : IDbTypeLogic
        {
            public void GetDetail()
            {
                Console.WriteLine("this is Sqlserver dbInstance");
            }

            public string GetMessage()
            {
                return "sqlserver";
            }
        }
        class MongodbLogic : IDbTypeLogic
        {
            public void GetDetail()
            {
                Console.WriteLine("this is Mongodb dbInstance");
            }

            public string GetMessage()
            {
                return "Mongodb";
            }
        }

        private Dictionary<DBType, Func<IDbTypeLogic>> dbTypeLogicFuncDicList = new Dictionary<DBType, Func<IDbTypeLogic>>();
        public void InitFuncList()
        {
            dbTypeLogicFuncDicList.Add(DBType.SqlServer, () => { return new SqlServerLogic(); });
            dbTypeLogicFuncDicList.Add(DBType.MongoDB, () => { return new MongodbLogic(); });
            dbTypeLogicFuncDicList.Add(DBType.MySql, () => { return new MysqlLogic(); });
        }
        public void ShowMessageD(string Dbtype)
        {
            this.dbType = (DBType)Enum.Parse(typeof(DBType), Dbtype);
            IDbTypeLogic typeLogic = dbTypeLogicFuncDicList[this.dbType]();
            typeLogic.GetDetail();
            Console.WriteLine(typeLogic.GetMessage());
        }
        public void Print()
        {
            //优化前
            ShowMessageA("SqlServer");

            //优化后
            ShowMessageB("SqlServer");

            //进一步优化
            Init();//初始化,建立一个委托的集合列表
            ShowMessageC("SqlServer");

            //再进一步优化
            InitFuncList();
            ShowMessageD("SqlServer");
        }
    }
}
