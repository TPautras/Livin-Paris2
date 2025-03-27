using System;
using MySql.Data.MySqlClient;

namespace LivinParis.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected readonly string ConnectionString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";

        protected MySqlConnection GetConnection()
        {
            MySqlConnection connection = new MySqlConnection(ConnectionString);
            connection.Open();
            Console.WriteLine("Connection established");
            connection.Close();
            return new MySqlConnection(ConnectionString);
        }
    }
}