using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
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
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Commande
                        {
                            CommandeId = Convert.ToInt32(reader["Commande_Id"]),
                            CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                            ClientUsername = reader["Client_Username"].ToString(),
                            DateCreation = Convert.ToDateTime(reader["Commande_Date"]),
                        });
                    }
                }
            }
            return list;
        }
        public Commande GetById(int id)
        {
            Commande cmd = null;
            string query = "SELECT * FROM Commande WHERE Commande_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        cmd = new Commande
                        {
                            CommandeId = Convert.ToInt32(reader["Commande_Id"]),
                            EntrepriseId = Convert.ToInt32(reader["Entreprise_Id"]),
                            CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                            ClientUsername = reader["Client_Username"].ToString(),
                            DateCreation = Convert.ToDateTime(reader["Commande_Date"]),
                        };
                    }
                }
            }
            return cmd;
        }
        public void Insert(Commande entity)
        {
            string query = @"INSERT INTO Commande 
                             (Commande_Id, Entreprise_Id, Cuisinier_Username, Client_Username, Commande_Date)
                             VALUES (@Id, @EntrepriseId, @CuisinierUsername, @ClientUsername, @DateCreation);";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.CommandeId);
                command.Parameters.AddWithValue("@EntrepriseId", entity.EntrepriseId);
                command.Parameters.AddWithValue("@CuisinierUsername", entity.CuisinierUsername);
                command.Parameters.AddWithValue("@ClientUsername", entity.ClientUsername);
                command.Parameters.AddWithValue("@DateCreation", entity.DateCreation);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Commande entity)
        {
            string query = @"UPDATE Commande SET 
                             Entreprise_Id = @EntrepriseId,
                             Cuisinier_Username = @CuisinierUsername,
                             Client_Username = @ClientUsername
                             WHERE Commande_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@EntrepriseId", entity.EntrepriseId);
                command.Parameters.AddWithValue("@CuisinierUsername", entity.CuisinierUsername);
                command.Parameters.AddWithValue("@ClientUsername", entity.ClientUsername);
                command.Parameters.AddWithValue("@Id", entity.CommandeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Commande WHERE Commande_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
