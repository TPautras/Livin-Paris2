using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Security.Cryptography;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;
using CryptingUtils;

namespace SqlConnector.DataAccess
{
    public class ClientDataAccess : BaseDataAccess, IDataAccess<Client>
    {
        private static readonly byte[] EncryptionKey = Crypter.GenerateKey("LivinParisSecretKey2025");

        public List<Client> GetAll()
        {
            var list = new List<Client>();
            string query = "SELECT * FROM Clients";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string username = reader["Client_Username"].ToString();
                        string encryptedPwd = reader["Client_Password"].ToString();
                        string email = reader["Personne_Email"].ToString();
                        string passwordPlain;
                        try
                        {
                            passwordPlain = Crypter.Decrypt(encryptedPwd, EncryptionKey);
                        }
                        catch(CryptographicException)
                        {
                            passwordPlain = encryptedPwd;
                            using(var conn = GetConnection())
                            {
                                conn.Open();
                                using(var cmd = new MySqlCommand("UPDATE Clients SET Client_Password = @Password WHERE Client_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", username);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        list.Add(new Client
                        {
                            ClientUsername = username,
                            ClientPassword = passwordPlain,
                            PersonneEmail = email
                        });
                    }
                }
            }
            return list;
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Client GetByUsername(string username)
        {
            Client c = null;
            string query = "SELECT * FROM Clients WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        string encryptedPwd = reader["Client_Password"].ToString();
                        string usernameValue = reader["Client_Username"].ToString();
                        string emailValue = reader["Personne_Email"].ToString();
                        string passwordPlain;
                        try
                        {
                            passwordPlain = Crypter.Decrypt(encryptedPwd, EncryptionKey);
                        }
                        catch(CryptographicException)
                        {
                            passwordPlain = encryptedPwd;
                            using(var conn = GetConnection())
                            {
                                conn.Open();
                                using(var cmd = new MySqlCommand("UPDATE Clients SET Client_Password = @Password WHERE Client_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", usernameValue);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        c = new Client
                        {
                            ClientUsername = usernameValue,
                            ClientPassword = passwordPlain,
                            PersonneEmail = emailValue
                        };
                    }
                }
            }
            return c;
        }

        public void Insert(Client entity)
        {
            string query = @"INSERT INTO Clients 
                             (Client_Username, Client_Password, Personne_Email)
                             VALUES (@Username, @Password, @PersonneEmail)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                command.Parameters.AddWithValue("@Password", Crypter.Encrypt(entity.ClientPassword, EncryptionKey));
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Client entity)
        {
            string query = @"UPDATE Clients SET 
                             Client_Password = @Password,
                             Personne_Email = @PersonneEmail
                             WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", Crypter.Encrypt(entity.ClientPassword, EncryptionKey));
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUsername(Client entity)
        {
            Client existingClient = null;
            using(var connection = GetConnection())
            {
                string findQuery = "SELECT * FROM Clients WHERE Client_Password = @Password";
                using(var findCommand = new MySqlCommand(findQuery, connection))
                {
                    connection.Open();
                    findCommand.Parameters.AddWithValue("@Password", Crypter.Encrypt(entity.ClientPassword, EncryptionKey));
                    using(var reader = findCommand.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            existingClient = new Client
                            {
                                ClientUsername = reader["Client_Username"].ToString(),
                                ClientPassword = entity.ClientPassword,
                                PersonneEmail = reader["Personne_Email"].ToString()
                            };
                        }
                    }
                }
                if(existingClient == null)
                {
                    connection.Close();
                    connection.Open();
                    using(var findCommand2 = new MySqlCommand("SELECT * FROM Clients WHERE Client_Password = @Password", connection))
                    {
                        findCommand2.Parameters.AddWithValue("@Password", entity.ClientPassword);
                        using(var reader2 = findCommand2.ExecuteReader())
                        {
                            if(reader2.Read())
                            {
                                existingClient = new Client
                                {
                                    ClientUsername = reader2["Client_Username"].ToString(),
                                    ClientPassword = entity.ClientPassword,
                                    PersonneEmail = reader2["Personne_Email"].ToString()
                                };
                            }
                        }
                    }
                }
                if(existingClient != null)
                {
                    string oldUsername = existingClient.ClientUsername;
                    connection.Close();
                    connection.Open();
                    string updateQuery = @"UPDATE Clients SET 
                                            Client_Username = @Username,
                                            Personne_Email = @Email,
                                            Client_Password = @Password
                                            WHERE Client_Username = @OldUsername";
                    using(var updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Username", entity.ClientUsername);
                        updateCommand.Parameters.AddWithValue("@Email", entity.PersonneEmail);
                        updateCommand.Parameters.AddWithValue("@Password", Crypter.Encrypt(existingClient.ClientPassword, EncryptionKey));
                        updateCommand.Parameters.AddWithValue("@OldUsername", oldUsername);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUsername(string username)
        {
            string query = "DELETE FROM Clients WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Client GetByEmail(string email)
        {
            Client c = null;
            string query = "SELECT * FROM Clients WHERE Personne_Email = @email";
            try
            {
                using(var connection = GetConnection())
                using(var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email.Trim());
                    connection.Open();
                    using(var reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            string usernameValue = reader["Client_Username"]?.ToString();
                            string encryptedPwd = reader["Client_Password"]?.ToString();
                            string emailValue = reader["Personne_Email"]?.ToString();
                            string passwordPlain;
                            try
                            {
                                passwordPlain = Crypter.Decrypt(encryptedPwd, EncryptionKey);
                            }
                            catch(CryptographicException)
                            {
                                passwordPlain = encryptedPwd;
                                using(var conn = GetConnection())
                                {
                                    conn.Open();
                                    using(var cmd = new MySqlCommand("UPDATE Clients SET Client_Password = @Password WHERE Client_Username = @Username", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                        cmd.Parameters.AddWithValue("@Username", usernameValue);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            c = new Client
                            {
                                ClientUsername = usernameValue,
                                ClientPassword = passwordPlain,
                                PersonneEmail = emailValue
                            };
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erreur SQL : " + ex.Message);
            }
            return c;
        }

        public List<Client> GetAllByNameAsc()
        {
            var list = new List<Client>();
            string query = @"
                SELECT c.Client_Username, c.Client_Password, c.Personne_Email
                FROM Clients c
                JOIN Personne p ON c.Personne_Email = p.Personne_Email
                ORDER BY p.Personne_Nom, p.Personne_Prenom";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string username = reader["Client_Username"].ToString();
                        string encryptedPwd = reader["Client_Password"].ToString();
                        string email = reader["Personne_Email"].ToString();
                        string passwordPlain;
                        try
                        {
                            passwordPlain = Crypter.Decrypt(encryptedPwd, EncryptionKey);
                        }
                        catch(CryptographicException)
                        {
                            passwordPlain = encryptedPwd;
                            using(var conn = GetConnection())
                            {
                                conn.Open();
                                using(var cmd = new MySqlCommand("UPDATE Clients SET Client_Password = @Password WHERE Client_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", username);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        list.Add(new Client
                        {
                            ClientUsername = username,
                            ClientPassword = passwordPlain,
                            PersonneEmail = email
                        });
                    }
                }
            }
            return list;
        }

        public List<Client> GetAllByStreetAsc()
        {
            var list = new List<Client>();
            string query = @"
                SELECT c.Client_Username, c.Client_Password, c.Personne_Email
                FROM Clients c
                JOIN Personne p ON c.Personne_Email = p.Personne_Email
                ORDER BY p.Personne_Nom_de_la_rue";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string username = reader["Client_Username"].ToString();
                        string encryptedPwd = reader["Client_Password"].ToString();
                        string email = reader["Personne_Email"].ToString();
                        string passwordPlain;
                        try
                        {
                            passwordPlain = Crypter.Decrypt(encryptedPwd, EncryptionKey);
                        }
                        catch(CryptographicException)
                        {
                            passwordPlain = encryptedPwd;
                            using(var conn = GetConnection())
                            {
                                conn.Open();
                                using(var cmd = new MySqlCommand("UPDATE Clients SET Client_Password = @Password WHERE Client_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", username);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        list.Add(new Client
                        {
                            ClientUsername = username,
                            ClientPassword = passwordPlain,
                            PersonneEmail = email
                        });
                    }
                }
            }
            return list;
        }

        public List<Client> GetAllByTotalPurchasesDesc()
        {
            var list = new List<Client>();
            string query = @"
                SELECT c.Client_Username, c.Client_Password, c.Personne_Email,
                       IFNULL(SUM(CAST(p.Plat_Prix AS DECIMAL(10,2))), 0) AS TotalSpent
                FROM Clients c
                LEFT JOIN Commande co ON c.Client_Username = co.Client_Username
                LEFT JOIN Creation cr ON co.Commande_Id = cr.Commande_Id
                LEFT JOIN Plat p ON cr.Plat_Id = p.Plat_Id
                GROUP BY c.Client_Username
                ORDER BY TotalSpent DESC";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string username = reader["Client_Username"].ToString();
                        string encryptedPwd = reader["Client_Password"].ToString();
                        string email = reader["Personne_Email"].ToString();
                        string passwordPlain;
                        try
                        {
                            passwordPlain = Crypter.Decrypt(encryptedPwd, EncryptionKey);
                        }
                        catch(CryptographicException)
                        {
                            passwordPlain = encryptedPwd;
                            using(var conn = GetConnection())
                            {
                                conn.Open();
                                using(var cmd = new MySqlCommand("UPDATE Clients SET Client_Password = @Password WHERE Client_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", username);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        list.Add(new Client
                        {
                            ClientUsername = username,
                            ClientPassword = passwordPlain,
                            PersonneEmail = email
                        });
                    }
                }
            }
            return list;
        }
    }
}
