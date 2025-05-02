using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class EntrepriseDataAccess : BaseDataAccess, IDataAccess<Entreprise>
    {
        public List<Entreprise> GetAll()
        {
            var list = new List<Entreprise>();
            string query = "SELECT * FROM Entreprise";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Entreprise
                        {
                            EntrepriseId = Convert.ToInt32(reader["Entreprise_Id"]),
                            EntrepriseNom = reader["Entreprise_Nom"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Entreprise GetById(int id)
        {
            Entreprise entreprise = null;
            string query = "SELECT * FROM Entreprise WHERE Entreprise_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        entreprise = new Entreprise
                        {
                            EntrepriseId = Convert.ToInt32(reader["Entreprise_Id"]),
                            EntrepriseNom = reader["Entreprise_Nom"].ToString()
                        };
                    }
                }
            }
            return entreprise;
        }

        public void Insert(Entreprise entity)
        {
            string query = "INSERT INTO Entreprise (Entreprise_Id, Entreprise_Nom) VALUES (@Id, @Nom)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.EntrepriseId);
                command.Parameters.AddWithValue("@Nom", entity.EntrepriseNom);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Entreprise entity)
        {
            string query = "UPDATE Entreprise SET Entreprise_Nom = @Nom WHERE Entreprise_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nom", entity.EntrepriseNom);
                command.Parameters.AddWithValue("@Id", entity.EntrepriseId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Entreprise WHERE Entreprise_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Personne GetByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
