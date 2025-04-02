using System;
using System.IO;
using System.Runtime.ConstrainedExecution;
using MySql.Data.MySqlClient;

namespace DBConnectionLibrary.Sql
{
    public class InitializeSql
    {
        public static void Initialize()
        {
            string connectionString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                ExecuteScript(connection, "../../../DBConnectionLibrary/Sql/schema.sql");
                ExecuteScript(connection, "../../../DBConnectionLibrary/Sql/sampleData.sql");
                ExecuteScript(connection, "../../../DBConnectionLibrary/Sql/setup_livinparis.sql");
                connection.Close();
            }
            Console.WriteLine("Database initialized.");
        }

        private static void ExecuteScript(MySqlConnection connection, string fileName)
        {
            string script = File.ReadAllText(fileName);
            string[] commands = script.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string command in commands)
            {
                if (!string.IsNullOrWhiteSpace(command))
                {
                    using (MySqlCommand cmd = new MySqlCommand(command, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}