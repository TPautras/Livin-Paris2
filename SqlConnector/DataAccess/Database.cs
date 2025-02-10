using System;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class Database
    {
        private readonly string _connectionString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";//Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public MySqlConnection GetConnection()
        {
            Console.WriteLine("Connecting to database...");
            Console.WriteLine(_connectionString);
            Console.WriteLine("TEST");
            return new MySqlConnection(_connectionString);
        }
    }
}
