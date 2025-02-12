using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class LivraisonDataAccess : IDataAccess<Livraison>
    {
        private readonly Database _database = new Database();

        public List<Livraison> GetAll()
        {
            var result = new List<Livraison>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Livraison";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var l = new Livraison
                        {
                            LivraisonId = reader.GetInt32("Livraison_Id"),
                            LivraisonAdresse = reader["Livraison_Adresse"] as string,
                            LivraisonDate = reader["Livraison_Date"] == DBNull.Value
                                ? (DateTime?)null
                                : reader.GetDateTime("Livraison_Date"),
                            CommandeId = reader["Commande_Id"] == DBNull.Value
                                ? null
                                : (int?)reader.GetInt32("Commande_Id")
                        };
                        result.Add(l);
                    }
                }
            }
            return result;
        }

        public Livraison GetById(int id)
        {
            Livraison l = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Livraison WHERE Livraison_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            l = new Livraison
                            {
                                LivraisonId = reader.GetInt32("Livraison_Id"),
                                LivraisonAdresse = reader["Livraison_Adresse"] as string,
                                LivraisonDate = reader["Livraison_Date"] == DBNull.Value
                                    ? (DateTime?)null
                                    : reader.GetDateTime("Livraison_Date"),
                                CommandeId = reader["Commande_Id"] == DBNull.Value
                                    ? null
                                    : (int?)reader.GetInt32("Commande_Id")
                            };
                        }
                    }
                }
            }
            return l;
        }

        public void Insert(Livraison entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Livraison 
                    (Livraison_Id, Livraison_Adresse, Livraison_Date, Commande_Id) 
                    VALUES (@id, @adresse, @date, @commande)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.LivraisonId);
                    cmd.Parameters.AddWithValue("@adresse", (object)entity.LivraisonAdresse ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", (object)entity.LivraisonDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@commande", (object)entity.CommandeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Livraison entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Livraison SET 
                    Livraison_Adresse = @adresse, 
                    Livraison_Date = @date, 
                    Commande_Id = @commande
                    WHERE Livraison_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.LivraisonId);
                    cmd.Parameters.AddWithValue("@adresse", (object)entity.LivraisonAdresse ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@date", (object)entity.LivraisonDate ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@commande", (object)entity.CommandeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Livraison WHERE Livraison_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}