using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class RecetteDataAccess : BaseDataAccess, IDataAccess<Recette>
    {
        public List<Recette> GetAll()
        {
            var list = new List<Recette>();
            string query = "SELECT * FROM Recette";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Recette
                        {
                            RecetteId = Convert.ToInt32(reader["Recette_id"]),
                            RecetteNom = reader["Recette_Nom"].ToString(),
                            RecetteOrigine = reader["Recette_Origine"].ToString(),
                            RecetteTypeDePlat = reader["Recette_Type_de_plat"].ToString(),
                            RecetteApportNutritifs = reader["Recette_Apport_nutritifs"].ToString(),
                            RecetteRegimeAlimentaire = reader["Recette_Regime_alimentaire"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Recette GetById(int id)
        {
            Recette recette = null;
            string query = "SELECT * FROM Recette WHERE Recette_id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        recette = new Recette
                        {
                            RecetteId = Convert.ToInt32(reader["Recette_id"]),
                            RecetteNom = reader["Recette_Nom"].ToString(),
                            RecetteOrigine = reader["Recette_Origine"].ToString(),
                            RecetteTypeDePlat = reader["Recette_Type_de_plat"].ToString(),
                            RecetteApportNutritifs = reader["Recette_Apport_nutritifs"].ToString(),
                            RecetteRegimeAlimentaire = reader["Recette_Regime_alimentaire"].ToString()
                        };
                    }
                }
            }
            return recette;
        }

        public void Insert(Recette entity)
        {
            string query = @"INSERT INTO Recette 
                             (Recette_id, Recette_Nom, Recette_Origine, Recette_Type_de_plat, Recette_Apport_nutritifs, Recette_Regime_alimentaire)
                             VALUES (@Id, @Nom, @Origine, @TypePlat, @Apport, @Regime)";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.RecetteId);
                command.Parameters.AddWithValue("@Nom", entity.RecetteNom);
                command.Parameters.AddWithValue("@Origine", entity.RecetteOrigine);
                command.Parameters.AddWithValue("@TypePlat", entity.RecetteTypeDePlat);
                command.Parameters.AddWithValue("@Apport", entity.RecetteApportNutritifs);
                command.Parameters.AddWithValue("@Regime", entity.RecetteRegimeAlimentaire);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Recette entity)
        {
            string query = @"UPDATE Recette SET 
                             Recette_Nom = @Nom, 
                             Recette_Origine = @Origine, 
                             Recette_Type_de_plat = @TypePlat, 
                             Recette_Apport_nutritifs = @Apport, 
                             Recette_Regime_alimentaire = @Regime
                             WHERE Recette_id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nom", entity.RecetteNom);
                command.Parameters.AddWithValue("@Origine", entity.RecetteOrigine);
                command.Parameters.AddWithValue("@TypePlat", entity.RecetteTypeDePlat);
                command.Parameters.AddWithValue("@Apport", entity.RecetteApportNutritifs);
                command.Parameters.AddWithValue("@Regime", entity.RecetteRegimeAlimentaire);
                command.Parameters.AddWithValue("@Id", entity.RecetteId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Recette WHERE Recette_id = @Id";
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
