using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class UtilisateurDao : IDataAccess<User>
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        public UtilisateurDao(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetAll()
        {
            List<User> utilisateurs = new List<User>();
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Utilisateurs", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        utilisateurs.Add(new User
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("nom"),
                            Prenom = reader.GetString("prenom"),
                            Email = reader.GetString("email"),
                            Telephone = reader.GetString("telephone"),
                            Adresse = reader.GetString("adresse"),
                            Type = reader.GetString("type"),
                            Password = reader.GetString("password")
                        });
                    }
                }
            }
            return utilisateurs;
        }

        public User GetById(int id)
        {
            User utilisateur = null;
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("SELECT * FROM Utilisateurs WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        utilisateur = new User
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("nom"),
                            Prenom = reader.GetString("prenom"),
                            Email = reader.GetString("email"),
                            Telephone = reader.GetString("telephone"),
                            Adresse = reader.GetString("adresse"),
                            Type = reader.GetString("type"),
                            Password = reader.GetString("password")
                        };
                    }
                }
            }
            return utilisateur;
        }

        public void Insert(User utilisateur)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("INSERT INTO Utilisateurs (nom, prenom, email, telephone, adresse, type, password) VALUES (@nom, @prenom, @email, @telephone, @adresse, @type, @password)", connection);
                command.Parameters.AddWithValue("@nom", utilisateur.Nom);
                command.Parameters.AddWithValue("@prenom", utilisateur.Prenom);
                command.Parameters.AddWithValue("@email", utilisateur.Email);
                command.Parameters.AddWithValue("@telephone", utilisateur.Telephone);
                command.Parameters.AddWithValue("@adresse", utilisateur.Adresse);
                command.Parameters.AddWithValue("@type", utilisateur.Type);
                command.Parameters.AddWithValue("@password", utilisateur.Password);
                command.ExecuteNonQuery();
            }
        }

        public void Update(User utilisateur)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("UPDATE Utilisateurs SET nom = @nom, prenom = @prenom, email = @email, telephone = @telephone, adresse = @adresse, type = @type, password = @password WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", utilisateur.Id);
                command.Parameters.AddWithValue("@nom", utilisateur.Nom);
                command.Parameters.AddWithValue("@prenom", utilisateur.Prenom);
                command.Parameters.AddWithValue("@email", utilisateur.Email);
                command.Parameters.AddWithValue("@telephone", utilisateur.Telephone);
                command.Parameters.AddWithValue("@adresse", utilisateur.Adresse);
                command.Parameters.AddWithValue("@type", utilisateur.Type);
                command.Parameters.AddWithValue("@password", utilisateur.Password);
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var command = new MySqlCommand("DELETE FROM Utilisateurs WHERE id = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }
}