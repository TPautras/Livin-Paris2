using System;
using MySql.Data.MySqlClient;

namespace LivinParis.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected readonly string ConnectionString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}