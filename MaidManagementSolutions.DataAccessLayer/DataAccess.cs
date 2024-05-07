namespace MaidManagementSolutions.DataAccessLayer
{
    using Npgsql;
    using System;
    using System.Configuration;
    using System.Data;

    public static class DataAccess
    {
        public static string GetConnectionString()
        {
            string connection = string.Format("Server=localhost;Database=maidfolio;port=5432;User Id=postgres;Password=root@123;");
            return connection;
        }
        public static void AdaptTheCommand(NpgsqlCommand command, object param)
        {
            if (param == null) return;
            var pInfos = param.GetType().GetProperties();
            foreach (var pInfo in pInfos)
            {
                var pVal = pInfo.GetValue(param);
                command.Parameters.AddWithValue("@" + pInfo.Name, pVal);
            }
        }
        /// <summary>
        /// Example of param
        /// var param = new
        ///    {
        ///        @actor_id = 1,
        ///        @first_name = "penelope"
        ///    };
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string sql, Object param)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {
                    DataAccess.AdaptTheCommand(mycommand, param);
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(mycommand))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(dt);
                        adapter.Dispose();
                        cnn.Close();
                        return dt;
                    }
                }
            }
        }
        public static DataTable InsertOneRecord(string sql)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(mycommand))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(dt);
                        adapter.Dispose();
                        cnn.Close();
                        return dt;
                    }
                }
            }
        }
        public static string ExecuteScalar(string sql, Object param)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {
                    DataAccess.AdaptTheCommand(mycommand, param);
                    object value = mycommand.ExecuteScalar();
                    cnn.Close();
                    if (value != null) return value.ToString();
                    return "";
                }
            }
        }
        public static string ExecuteScalar(string sql)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {
                    object value = null;
                    try
                    {
                        value = mycommand.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        value = ex.Message.ToString();
                    }
                    cnn.Close();
                    if (value != null) return value.ToString();
                    return "";
                }
            }
        }
        public static int ExecuteNonQuery(string sql, Object param)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    DataAccess.AdaptTheCommand(command, param);
                    int value = command.ExecuteNonQuery();
                    connection.Close();
                    return value;
                }
            }
        }
        public static DataTable GetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(mycommand))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        adapter.Fill(dt);
                        adapter.Dispose();
                        cnn.Close();
                        return dt;
                    }
                }
            }
        }
        public static int InsertSQL(string sql)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {
                    int value = mycommand.ExecuteNonQuery();
                    cnn.Close();
                    return value;
                }
            }
        }
        public static int DeleteSQL(string sql)
        {
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {
                    int value = mycommand.ExecuteNonQuery();
                    cnn.Close();
                    return value;
                }
            }
        }
        public static DataTable AddProjectTable(string sql)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (NpgsqlCommand mycommand = new NpgsqlCommand(sql, cnn))
                {

                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(mycommand))
                    {
                        adapter.AcceptChangesDuringFill = true;
                        try
                        {
                            adapter.Fill(dt);
                            adapter.Dispose();
                            cnn.Close();

                        }
                        catch (Exception)
                        {
                            dt = null;

                        }
                        return dt;
                    }
                }
            }
        }
        public static DataTable GetDataTable(NpgsqlCommand mycommand)
        {
            DataTable dt = new DataTable();
            using (NpgsqlConnection cnn = new NpgsqlConnection(GetConnectionString()))
            {
                cnn.Open();
                mycommand.Connection = cnn;
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(mycommand))
                {
                    adapter.AcceptChangesDuringFill = true;
                    adapter.Fill(dt);
                    adapter.Dispose();
                    cnn.Close();
                    mycommand.Dispose();
                    return dt;
                }

            }
        }
        public static int UpDdateSQl(string sql, Object param)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
            {
                connection.Open();
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    DataAccess.AdaptTheCommand(command, param);
                    int value = command.ExecuteNonQuery();
                    connection.Close();
                    return value;
                }
            }
        }
    }
}
