using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace generics.example
{
    // 进一步优化，提取重复代码，采用泛型委托的方式的进行
    // 每个方法同存在sqlconnection的开启与sqlcommand的定义
    class BaseDALV2 : IBaseDAL
    {
        private static string strConn = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public int Delete<T>(int id) where T : BaseModel
        {
            string strSQl = "delete from student where Id=@id";
            Func<SqlCommand, int> func = (SqlCommand command) =>
            {
                SqlParameter param = new SqlParameter("id", id);
                command.Parameters.Add(param);
                return command.ExecuteNonQuery();
            };
            return ExcuteSql<int>(strSQl, func);
        }

        public int Insert<T>(T t) where T : BaseModel
        {
            Type type = typeof(T);
            var proArray = type.GetProperties().Where(p => p.Name != "Id");
            string strSQl = "insert into Student Values (@Name,@Age,@Sex,@Email) ";
            Func<SqlCommand, int> func = (SqlCommand command) =>
            {
                var parameters = proArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
                command.Parameters.AddRange(parameters);
                return command.ExecuteNonQuery();
            };
            return ExcuteSql<int>(strSQl, func);
        }

        public T Query<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            T t = default(T);
            DataTable dt = new DataTable();
            var proArray = type.GetProperties().Where(p => p.Name != "Id");
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"SELECT {columnString} FROM [{type.Name}] WHERE Id=@Id ";
            Func<SqlCommand, T> func = (SqlCommand command) =>
            {
                command.Parameters.Add(new SqlParameter("@Id", id));
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dt);
                List<T> list = this.ConvertToList<T>(dt);
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
            DataTable dt = new DataTable();
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"SELECT {columnString} FROM [{type.Name}] ";
            Func<SqlCommand, List<T>> func = (a) =>
             {
                 SqlDataAdapter adapter = new SqlDataAdapter(a);
                 adapter.Fill(dt);
                 list = this.ConvertToList<T>(dt);
                 return list;
             };
            list = ExcuteSql<List<T>>(sql, func);
            return list;
        }

        public int Update<T>(T t) where T : BaseModel
        {
            int result = 0;
            Type type = typeof(T);
            var proArray = type.GetProperties().Where(p => p.Name != "Id");
            var columnString = string.Join(",", proArray.Select(p => $"[{p.GetColumnName()}]=@{p.GetColumnName()}"));
            var parameters = proArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
            var strSQl = $"UPDATE [{type.Name}] SET {columnString} WHERE Id={t.Id}";
            Func<SqlCommand, int> func = (cmd) =>
            {
                cmd.Parameters.AddRange(parameters);
                result = cmd.ExecuteNonQuery();
                return result;
            };
            return result;
        }

        private List<T> ReaderToList<T>(SqlDataReader reader) where T : BaseModel
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
        private T ExcuteSql<T>(string sql, Func<SqlCommand, T> Func)
        {
            T t = default(T);
            using (SqlConnection conn = new SqlConnection(strConn))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    SqlTransaction transaction = conn.BeginTransaction();
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
    }
}
