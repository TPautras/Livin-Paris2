using System;
using CryptingUtils;
using MySql.Data.MySqlClient;

namespace LivinParis.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected readonly string ConnectionString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";
        protected static readonly byte[] EncryptionKey = Crypter.GenerateKey("LivinParisSecretKey2025");

        protected MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }
    }
}