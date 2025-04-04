using System;
using System.Collections.Generic;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;
namespace SqlConnector.DataAccess
{
    public class CuisinierDataAccess : BaseDataAccess, IDataAccess<Cuisinier>
    {
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
                        list.Add(new Cuisinier
                        {
                            CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                            CuisinierPassword = reader["Cuisinier_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
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
                command.Parameters.AddWithValue("@Password", entity.CuisinierPassword);
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
                command.Parameters.AddWithValue("@Password", entity.CuisinierPassword);
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
                        c = new Cuisinier
                        {
                            CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                            CuisinierPassword = reader["Cuisinier_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
                        };
                    }
                }
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
            string query = @"UPDATE Cuisinier SET 
                             Cuisinier_Username = @Username,
                             Personne_Email = @PersonneEmail
                             WHERE Cuisinier_Password = @Password";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", entity.CuisinierPassword);
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Username", entity.CuisinierUsername);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
