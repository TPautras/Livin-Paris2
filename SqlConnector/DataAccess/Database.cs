using System;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class Database
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
