using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace generics.example
{
    public class BaseDAL : IBaseDAL
    {
        private static string strConn = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;
        public int Delete<T>(int id) where T : BaseModel
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                string strSQl = "delete from student where Id=@id";
                SqlParameter param = new SqlParameter("id", id);
                SqlCommand command = new SqlCommand(strSQl, connection);
                command.Parameters.Add(param);
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public int Insert<T>(T t) where T : BaseModel
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                Type type = typeof(T);
                var proArray = type.GetProperties().Where(p => p.Name != "Id");
                string strSQl = "insert into Student Values (@Name,@Age,@Sex,@Email) ";
                SqlCommand command = new SqlCommand(strSQl, connection);
                var parameters = proArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
                command.Parameters.AddRange(parameters);
                connection.Open();
                result = command.ExecuteNonQuery();
            }
            return result;
        }

        public T Query<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);
            T t = null;
            var proArray = type.GetProperties().Where(p => p.Name != "Id");
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"SELECT {columnString} FROM [{type.Name}] WHERE Id={id}";
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                List<T> list = this.ReaderToList<T>(reader);
                t = list.FirstOrDefault();
            }
            return t;
        }

        public List<T> QueryAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            string columnString = string.Join(",", type.GetProperties().Select(p => $"[{p.GetColumnName()}]"));
            string sql = $"SELECT {columnString} FROM [{type.Name}] ";
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                list = this.ReaderToList<T>(reader);
            }
            return list;
        }

        public int Update<T>(T t) where T : BaseModel
        {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(strConn))
            {
                Type type = typeof(T);
                var proArray = type.GetProperties().Where(p => p.Name != "Id");
                var columnString = string.Join(",", proArray.Select(p => $"[{p.GetColumnName()}]=@{p.GetColumnName()}"));
                var parameters = proArray.Select(p => new SqlParameter($"@{p.GetColumnName()}", p.GetValue(t) ?? DBNull.Value)).ToArray();
                var strSQl = $"UPDATE [{type.Name}] SET {columnString} WHERE Id={t.Id}";

                SqlCommand command = new SqlCommand(strSQl, connection);
                command.Parameters.AddRange(parameters);
                connection.Open();
                result = command.ExecuteNonQuery();
            }
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
    }
}
