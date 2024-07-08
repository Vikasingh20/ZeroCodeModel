
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Text;

namespace Database
{
    public static class GetObjects
    {
        public static List<Dictionary<string, string>> ObjectDictionaryByRawSql(this DbContext dbContext, string sql)
        {
            int result = -1;
            SqlConnection connection = dbContext.Database.Connection as SqlConnection;
            List<Dictionary<string, string>> keyValuePairs = new List<Dictionary<string, string>>();
            try
            {
                connection.Open();

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = sql;

                    //foreach (KeyValuePair<string, object> parameter in parameters)
                    //    command.Parameters.AddWithValue(parameter.Key, parameter.Value);

                    using (DbDataReader dataReader = command.ExecuteReader())
                        if (dataReader.HasRows)
                            while (dataReader.Read())
                            {
                                var keyValuePair = new Dictionary<string, string>();
                                for (int i = 0; i < dataReader.FieldCount; i++)
                                {
                                    var val = dataReader.GetValue(i);
                                    keyValuePair.Add(dataReader.GetName(i).ToLower(), val == null ? "" : val.ToString());
                                }
                                keyValuePairs.Add(keyValuePair);
                            }

                }
            }

            // We should have better error handling here
            catch (System.Exception e) {
                throw e;
            }

            finally { connection.Close(); }

            return keyValuePairs;
        }
    }
}
