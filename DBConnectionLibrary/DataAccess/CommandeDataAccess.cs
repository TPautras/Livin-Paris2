using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class CommandeDataAccess : BaseDataAccess, IDataAccess<Commande>
    {
        public List<Commande> GetAll()
        {
            var list = new List<Commande>();
            string query = "SELECT * FROM Commande";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Commande
                        {
                            CommandeId = Convert.ToInt32(reader["Commande_Id"]),
                            EntrepriseId = Convert.ToInt32(reader["Entreprise_Id"]),
                            CuisinierId = Convert.ToInt32(reader["Cuisinier_Id"]),
                            ClientId = Convert.ToInt32(reader["Client_Id"])
                        });
                    }
                }
            }
            return list;
        }

        public Commande GetById(int id)
        {
            Commande commande = null;
            string query = "SELECT * FROM Commande WHERE Commande_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        commande = new Commande
                        {
                            CommandeId = Convert.ToInt32(reader["Commande_Id"]),
                            EntrepriseId = Convert.ToInt32(reader["Entreprise_Id"]),
                            CuisinierId = Convert.ToInt32(reader["Cuisinier_Id"]),
                            ClientId = Convert.ToInt32(reader["Client_Id"])
                        };
                    }
                }
            }
            return commande;
        }

        public void Insert(Commande entity)
        {
            string query = "INSERT INTO Commande (Commande_Id, Entreprise_Id, Cuisinier_Id, Client_Id) " +
                           "VALUES (@Id, @EntrepriseId, @CuisinierId, @ClientId)";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.CommandeId);
                command.Parameters.AddWithValue("@EntrepriseId", entity.EntrepriseId);
                command.Parameters.AddWithValue("@CuisinierId", entity.CuisinierId);
                command.Parameters.AddWithValue("@ClientId", entity.ClientId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Commande entity)
        {
            string query = "UPDATE Commande SET Entreprise_Id = @EntrepriseId, Cuisinier_Id = @CuisinierId, Client_Id = @ClientId " +
                           "WHERE Commande_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EntrepriseId", entity.EntrepriseId);
                command.Parameters.AddWithValue("@CuisinierId", entity.CuisinierId);
                command.Parameters.AddWithValue("@ClientId", entity.ClientId);
                command.Parameters.AddWithValue("@Id", entity.CommandeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Commande WHERE Commande_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
