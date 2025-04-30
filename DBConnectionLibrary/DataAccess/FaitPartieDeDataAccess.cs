using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class FaitPartieDeDataAccess : BaseDataAccess, IDataAccess<FaitPartieDe>
    {
        public List<FaitPartieDe> GetAll()
        {
            var list = new List<FaitPartieDe>();
            string query = "SELECT * FROM Fait_Partie_De";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new FaitPartieDe
                        {
                            PersonneId = reader["Personne_Email"].ToString(),
                            EntrepriseId = Convert.ToInt32(reader["Entreprise_Id"])
                        });
                    }
                }
            }
            return list;
        }

        public FaitPartieDe GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite.");
        }

        public void Insert(FaitPartieDe entity)
        {
            string query = "INSERT INTO Fait_Partie_De (Personne_Email, Entreprise_Id) VALUES (@PersonneId, @EntrepriseId)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonneId", entity.PersonneId);
                command.Parameters.AddWithValue("@EntrepriseId", entity.EntrepriseId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(FaitPartieDe entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite.");
        }
    }
}
