using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class ClientDataAccess : IDataAccess<Client>
    {
        private readonly Database _database = new Database();

        public List<Client> GetAll()
        {
            var result = new List<Client>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Clients";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new Client
                        {
                            PersonneId = reader.GetInt32("Client_Id")
                        };
                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public Client GetById(int id)
        {
            Client c = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Clients WHERE Client_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            c = new Client
                            {
                                PersonneId = reader.GetInt32("Client_Id")
                            };
                        }
                    }
                }
            }

            return c;
        }

        public void Insert(Client entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Clients (Client_Id) VALUES (@id)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.PersonneId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Client entity)
        {
            Console.WriteLine("WORK IN PROGRESS");
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Clients WHERE Client_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}