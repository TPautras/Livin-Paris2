using System;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class Database
    {
        private readonly string connectionString = "Server=localhost;Port=3306;Database=livin_paris;User Id=root;Password=root;";

        public MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
