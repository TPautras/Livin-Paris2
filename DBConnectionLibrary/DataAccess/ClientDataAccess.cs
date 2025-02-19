using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using SqlConnector.Models;
namespace SqlConnector.DataAccess
{
    public class ClientDataAccess : BaseDataAccess, IDataAccess<Client>
    {
        public List<Client> GetAll()
        {
            var list = new List<Client>();
            string query = "SELECT * FROM Clients";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
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
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        c = new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
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
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                command.Parameters.AddWithValue("@Password", entity.ClientPassword);
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
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", entity.ClientPassword);
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Delete(string username)
        {
            string query = "DELETE FROM Clients WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
