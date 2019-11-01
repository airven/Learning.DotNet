using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
namespace generics.example
{
    public class BaseDALV3 : IBaseDAL
    {
        private static string strConn = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        private static readonly DbProviderFactory factory;
        static BaseDALV3()
        {
            factory = DbProviderFactories.GetFactory("System.Data.SqlClient");
        }
        public int Delete<T>(int id) where T : BaseModel
        {
            string strSQl = "delete from student where Id=@id";
            Func<IDbCommand, int> func = (command) =>
            {
                IDbDataParameter param = factory.CreateParameter();
                param.ParameterName = "id";
                param.Value = id;
                command.Parameters.Add(param);
                return command.ExecuteNonQuery();
            };
            return ExcuteSql<int>(strSQl, func);
        }
        public T Query<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            T t = default(T);
            DataSet dt = new DataSet();
            var proArray = type.GetProperties().Where(p => p.Name != "Id");
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"SELECT {columnString} FROM [{type.Name}] WHERE Id=@Id ";
            Func<IDbCommand, T> func = (command) =>
            {
                IDbDataParameter param = factory.CreateParameter();
                param.ParameterName = "id";
                param.Value = id;
                command.Parameters.Add(param);
                DataAdapter adapter = factory.CreateDataAdapter();
                adapter.Fill(dt);
                List<T> list = this.ConvertToList<T>(dt.Tables[0]);
                T tResult = list.FirstOrDefault();
                return tResult;
            };
            t = ExcuteSql<T>(sql, func);
            return t;
        }

        public List<T> QueryAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            DataSet dt = new DataSet("setname");
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"SELECT {columnString} FROM [{type.Name}] ";
            Func<IDbCommand, List<T>> func = (a) =>
            {
                DataAdapter adapter = factory.CreateDataAdapter();
                adapter.Fill(dt);
                list = this.ConvertToList<T>(dt.Tables[0]);
                return list;
            };
            list = ExcuteSql<List<T>>(sql, func);
            return list;
        }
        private List<T> ReaderToList<T>(DbDataReader reader) where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            while (reader.Read())
            {
                T t = (T)Activator.CreateInstance(type);
                foreach (var prop in type.GetProperties())
                {
                    object oValue = reader[prop.GetColumnName()];
                    if (oValue is DBNull)
                        oValue = null;
                    prop.SetValue(t, oValue);
                }
                list.Add(t);
            }
            reader.Close();
            return list;
        }
        private List<T> ConvertToList<T>(DataTable dt) where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            foreach (DataRow dr in dt.Rows)
            {
                T t = (T)Activator.CreateInstance(type);
                foreach (PropertyInfo prop in type.GetProperties())
                {
                    object value = dr[prop.GetColumnName()];
                    if (value is DBNull)
                    {
                        value = null;
                    }
                    prop.SetValue(t, value);
                }
                list.Add(t);
            }
            return list;
        }
        private T ExcuteSql<T>(string sql, Func<IDbCommand, T> Func)
        {
            T t = default(T);
            using (IDbConnection conn = factory.CreateConnection())
            {
                conn.ConnectionString = strConn;
                conn.Open();
                using (IDbCommand cmd = factory.CreateCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    IDbTransaction transaction = conn.BeginTransaction();
                    try
                    {
                        cmd.Transaction = transaction;
                        t = Func.Invoke(cmd);
                        transaction.Commit();
                        return t;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
        public int Insert<T>(T t) where T : BaseModel
        {
            throw new NotImplementedException();
        }
        public int Update<T>(T t) where T : BaseModel
        {
            throw new NotImplementedException();
        }
    }
}
