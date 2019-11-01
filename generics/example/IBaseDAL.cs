using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace generics.example
{
    interface IBaseDAL
    {
        T Query<T>(int id) where T : BaseModel;
        List<T> QueryAll<T>() where T : BaseModel;
        int Insert<T>(T t) where T : BaseModel;
        int Update<T>(T t) where T : BaseModel;
        int Delete<T>(int id) where T : BaseModel;
    }
}
