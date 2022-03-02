using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jwt.Database
{
    public static class SQLHelper
    {
        public static string ConnectionString = string.Empty;
        public async static Task<IEnumerable<T>> CGetData<T>(string sql, List<SqlParameter> sqlParameters = null) where T : new()
        {
            var tcs = new TaskCompletionSource<List<T>>();
            DataSet ds = new DataSet();
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand(sql, con))
                {
                    if (sqlParameters != null)
                        cmd.Parameters.AddRange(sqlParameters.ToArray());
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        da.SelectCommand = cmd;
                        da.Fill(ds);
                    }
                    con.Close();
                    string JSONString = string.Empty;
                    JSONString = JsonConvert.SerializeObject(ds.Tables[0]);
                    IEnumerable<T> list = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(JSONString));
                    return list;
                }
            }
        }
    }
}
