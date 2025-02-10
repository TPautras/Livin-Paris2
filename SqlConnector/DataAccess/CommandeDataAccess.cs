using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class CommandeDataAccess : IDataAccess<Commande>
    {
        private readonly Database _database = new Database();
        
        public List<Commande> GetAll()
        {
            var result = new List<Commande>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Commande";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var commande = new Commande
                        {
                            CommandeId = reader.GetInt32("Commande_Id"),
                            CommandeDate = reader["Commande_Date"] == DBNull.Value 
                                ? (DateTime?)null 
                                : reader.GetDateTime("Commande_Date"),
                            ClientId = reader["Client_Id"] == DBNull.Value 
                                ? null 
                                : (int?)reader.GetInt32("Client_Id")
                        };
                        result.Add(commande);
                    }
                }
            }
            return result;
        }

        public Commande GetById(int id)
        {
            Commande commande = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Commande WHERE Commande_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            commande = new Commande
                            {
                                CommandeId = reader.GetInt32("Commande_Id"),
                                CommandeDate = reader["Commande_Date"] == DBNull.Value 
                                    ? (DateTime?)null 
                                    : reader.GetDateTime("Commande_Date"),
                                ClientId = reader["Client_Id"] == DBNull.Value 
                                    ? null 
                                    : (int?)reader.GetInt32("Client_Id")
                            };
                        }
                    }
                }
            }
            return commande;
        }

        public void Insert(Commande entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Commande (Commande_Id, Commande_Date, Client_Id) 
                              VALUES (@id, @date, @clientId)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.CommandeId);
                    cmd.Parameters.AddWithValue("@date", (object)entity.CommandeDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@clientId", (object)entity.ClientId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Commande entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Commande 
                              SET Commande_Date = @date, Client_Id = @clientId
                              WHERE Commande_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.CommandeId);
                    cmd.Parameters.AddWithValue("@date", (object)entity.CommandeDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@clientId", (object)entity.ClientId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Commande WHERE Commande_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}