using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class LivraisonDataAccess : BaseDataAccess, IDataAccess<Livraison>
    {
        public List<Livraison> GetAll()
        {
            var list = new List<Livraison>();
            string query = "SELECT * FROM Livraison";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Livraison
                        {
                            LivraisonId = Convert.ToInt32(reader["Livraison_Id"]),
                            LivraisonAdresse = reader["Livraison_Adresse"].ToString(),
                            LivraisonDate = Convert.ToDateTime(reader["Livraison_Date"])
                        });
                    }
                }
            }
            return list;
        }

        public Livraison GetById(int id)
        {
            Livraison livraison = null;
            string query = "SELECT * FROM Livraison WHERE Livraison_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        livraison = new Livraison
                        {
                            LivraisonId = Convert.ToInt32(reader["Livraison_Id"]),
                            LivraisonAdresse = reader["Livraison_Adresse"].ToString(),
                            LivraisonDate = Convert.ToDateTime(reader["Livraison_Date"])
                        };
                    }
                }
            }
            return livraison;
        }

        public void Insert(Livraison entity)
        {
            string query = "INSERT INTO Livraison (Livraison_Id, Livraison_Adresse, Livraison_Date) " +
                           "VALUES (@Id, @Adresse, @Date)";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.LivraisonId);
                command.Parameters.AddWithValue("@Adresse", entity.LivraisonAdresse);
                command.Parameters.AddWithValue("@Date", entity.LivraisonDate);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Livraison entity)
        {
            string query = "UPDATE Livraison SET Livraison_Adresse = @Adresse, Livraison_Date = @Date " +
                           "WHERE Livraison_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Adresse", entity.LivraisonAdresse);
                command.Parameters.AddWithValue("@Date", entity.LivraisonDate);
                command.Parameters.AddWithValue("@Id", entity.LivraisonId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Livraison WHERE Livraison_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
