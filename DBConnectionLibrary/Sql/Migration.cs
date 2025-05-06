using System;
using MySql.Data.MySqlClient;
using CryptingUtils;

namespace DBConnectionLibrary.Sql
{
    public class Migration
    {
        static readonly byte[] EncryptionKey = Crypter.GenerateKey("LivinParisSecretKey2025");

        public static void Main()
        {
            string connString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";

            MigrateTable(connString, "Clients", "Client_Username", "Client_Password");
            MigrateTable(connString, "Cuisinier", "Cuisinier_Username", "Cuisinier_Password");

            Console.WriteLine("Migration terminée.");
        }

        static void MigrateTable(string connStr, string table, string usernameCol, string passwordCol)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string selectQuery = $"SELECT {usernameCol}, {passwordCol} FROM {table}";
                using (var selectCmd = new MySqlCommand(selectQuery, conn))
                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string username = reader.GetString(0);
                        string passwordPlain = reader.GetString(1);
                        string passwordEncrypted = Crypter.Encrypt(passwordPlain, EncryptionKey);

                        UpdatePassword(connStr, table, usernameCol, passwordCol, username, passwordEncrypted);
                    }
                }
            }
        }

        static void UpdatePassword(string connStr, string table, string usernameCol, string passwordCol, string username, string newPassword)
        {
            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();
                string updateQuery = $"UPDATE {table} SET {passwordCol} = @Password WHERE {usernameCol} = @Username";
                using (var cmd = new MySqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
