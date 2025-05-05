using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;
using CryptingUtils;

namespace SqlConnector.DataAccess
{
    public class CuisinierDataAccess : BaseDataAccess, IDataAccess<Cuisinier>
    {
        private static readonly byte[] EncryptionKey = Crypter.GenerateKey("LivinParisSecretKey2025");

        public List<Cuisinier> GetAll()
        {
            var list = new List<Cuisinier>();
            string query = "SELECT * FROM Cuisinier";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        string username = reader["Cuisinier_Username"].ToString();
                        string encryptedPwd = reader["Cuisinier_Password"].ToString();
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
                                using(var cmd = new MySqlCommand("UPDATE Cuisinier SET Cuisinier_Password = @Password WHERE Cuisinier_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", username);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        list.Add(new Cuisinier
                        {
                            CuisinierUsername = username,
                            CuisinierPassword = passwordPlain,
                            PersonneEmail = email
                        });
                    }
                }
            }
            return list;
        }

        public Cuisinier GetById(int id)
        {
            throw new NotImplementedException("Utilisez GetByUsername(string username).");
        }

        public void Insert(Cuisinier entity)
        {
            string query = @"INSERT INTO Cuisinier 
                             (Cuisinier_Username, Cuisinier_Password, Personne_Email)
                             VALUES (@Username, @Password, @PersonneEmail)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", entity.CuisinierUsername);
                command.Parameters.AddWithValue("@Password", Crypter.Encrypt(entity.CuisinierPassword, EncryptionKey));
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Cuisinier entity)
        {
            string query = @"UPDATE Cuisinier SET 
                             Cuisinier_Password = @Password,
                             Personne_Email = @PersonneEmail
                             WHERE Cuisinier_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", Crypter.Encrypt(entity.CuisinierPassword, EncryptionKey));
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Username", entity.CuisinierUsername);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Utilisez DeleteByUsername(string username).");
        }

        public Cuisinier GetByUsername(string username)
        {
            Cuisinier c = null;
            string query = "SELECT * FROM Cuisinier WHERE Cuisinier_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        string encryptedPwd = reader["Cuisinier_Password"].ToString();
                        string usernameValue = reader["Cuisinier_Username"].ToString();
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
                                using(var cmd = new MySqlCommand("UPDATE Cuisinier SET Cuisinier_Password = @Password WHERE Cuisinier_Username = @Username", conn))
                                {
                                    cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                    cmd.Parameters.AddWithValue("@Username", usernameValue);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }
                        c = new Cuisinier
                        {
                            CuisinierUsername = usernameValue,
                            CuisinierPassword = passwordPlain,
                            PersonneEmail = emailValue
                        };
                    }
                }
            }
            return c;
        }

        public Cuisinier GetByEmail(string email)
        {
            Cuisinier c = null;
            string query = "SELECT * FROM Cuisinier WHERE Personne_Email = @email";
            try
            {
                using(var connection = GetConnection())
                using(var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email.Trim());
                    connection.Open();
                    using(var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string usernameValue = reader["Cuisinier_Username"]?.ToString();
                            string encryptedPwd = reader["Cuisinier_Password"]?.ToString();
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
                                    using(var cmd = new MySqlCommand("UPDATE Cuisinier SET Cuisinier_Password = @Password WHERE Cuisinier_Username = @Username", conn))
                                    {
                                        cmd.Parameters.AddWithValue("@Password", Crypter.Encrypt(passwordPlain, EncryptionKey));
                                        cmd.Parameters.AddWithValue("@Username", usernameValue);
                                        cmd.ExecuteNonQuery();
                                    }
                                }
                            }
                            c = new Cuisinier
                            {
                                CuisinierUsername = usernameValue,
                                CuisinierPassword = passwordPlain,
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

        public void DeleteByUsername(string username)
        {
            string query = "DELETE FROM Cuisinier WHERE Cuisinier_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void UpdateUsername(Cuisinier entity)
        {
            Cuisinier existingCuisinier = null;
            using(var connection = GetConnection())
            {
                string findQuery = "SELECT * FROM Cuisinier WHERE Cuisinier_Password = @Password";
                using(var findCommand = new MySqlCommand(findQuery, connection))
                {
                    connection.Open();
                    findCommand.Parameters.AddWithValue("@Password", Crypter.Encrypt(entity.CuisinierPassword, EncryptionKey));
                    using(var reader = findCommand.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            existingCuisinier = new Cuisinier
                            {
                                CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                                CuisinierPassword = entity.CuisinierPassword,
                                PersonneEmail = reader["Personne_Email"].ToString()
                            };
                        }
                    }
                }
                if(existingCuisinier == null)
                {
                    connection.Close();
                    connection.Open();
                    using(var findCommand2 = new MySqlCommand("SELECT * FROM Cuisinier WHERE Cuisinier_Password = @Password", connection))
                    {
                        findCommand2.Parameters.AddWithValue("@Password", entity.CuisinierPassword);
                        using(var reader2 = findCommand2.ExecuteReader())
                        {
                            if(reader2.Read())
                            {
                                existingCuisinier = new Cuisinier
                                {
                                    CuisinierUsername = reader2["Cuisinier_Username"].ToString(),
                                    CuisinierPassword = entity.CuisinierPassword,
                                    PersonneEmail = reader2["Personne_Email"].ToString()
                                };
                            }
                        }
                    }
                }
                if(existingCuisinier != null)
                {
                    string oldUsername = existingCuisinier.CuisinierUsername;
                    connection.Close();
                    connection.Open();
                    string updateQuery = @"UPDATE Cuisinier SET 
                                            Cuisinier_Username = @Username,
                                            Personne_Email = @Email,
                                            Cuisinier_Password = @Password
                                            WHERE Cuisinier_Username = @OldUsername";
                    using(var updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@Username", entity.CuisinierUsername);
                        updateCommand.Parameters.AddWithValue("@Email", entity.PersonneEmail);
                        updateCommand.Parameters.AddWithValue("@Password", Crypter.Encrypt(existingCuisinier.CuisinierPassword, EncryptionKey));
                        updateCommand.Parameters.AddWithValue("@OldUsername", oldUsername);
                        updateCommand.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
