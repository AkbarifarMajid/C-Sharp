using System;
using System.Configuration;
using MySql.Data.MySqlClient;  

namespace GolddiggerGmbh.DAL
{
    public static class SqlConnectionFactory
    {
        private static readonly string _cs =
            ConfigurationManager.ConnectionStrings["MyZDFConnection"]?.ConnectionString
            ?? throw new InvalidOperationException("Connection 'MyZDFConnection' not found in App.config.");

        public static MySqlConnection GetOpenConnection()
        {
            var conn = new MySqlConnection(_cs);
            conn.Open();
            return conn;
        }
    }
}
