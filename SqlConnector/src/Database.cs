using System;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class Database
    {
        private static string _connectionString = "Server=localhost;Port=3306;Database=livin_paris;User Id=root;Password=root;";

        public static void TestConnexion()
        {
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Connexion réussie à la base de données !");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur de connexion : {ex.Message}");
                }
            }
        }
    }
}
