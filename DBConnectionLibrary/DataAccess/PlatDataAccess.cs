using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;
namespace SqlConnector.DataAccess
{
    public class PlatDataAccess : BaseDataAccess, IDataAccess<Plat>
    {
        public List<Plat> GetAll()
        {
            var list = new List<Plat>();
            string query = "SELECT * FROM Plat";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        bool platDuJour = reader["Plat_Du_Jour"] != DBNull.Value && Convert.ToBoolean(reader["Plat_Du_Jour"]);
                        list.Add(new Plat
                        {
                            PlatId = Convert.ToInt32(reader["Plat_Id"]),
                            PlatDateDeFabrication = Convert.ToDateTime(reader["Plat_date_de_fabrication"]),
                            PlatDateDePeremption = Convert.ToDateTime(reader["Plat_Date_de_peremption"]),
                            PlatPrix = reader["Plat_Prix"].ToString(),
                            PlatNombrePortion = Convert.ToInt32(reader["Plat_Nombre_Portion"]),
                            CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                            RecetteId = Convert.ToInt32(reader["Recette_id"]),
                            PlatDuJour = platDuJour,
                        });
                    }
                }
            }
            return list;
        }
        public Plat GetById(int id)
        {
            Plat plat = null;
            string query = "SELECT * FROM Plat WHERE Plat_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        bool platDuJour = reader["Plat_Du_Jour"] != DBNull.Value && Convert.ToBoolean(reader["Plat_Du_Jour"]);
                        plat = new Plat
                        {
                            PlatId = Convert.ToInt32(reader["Plat_Id"]),
                            PlatDateDeFabrication = Convert.ToDateTime(reader["Plat_date_de_fabrication"]),
                            PlatDateDePeremption = Convert.ToDateTime(reader["Plat_Date_de_peremption"]),
                            PlatPrix = reader["Plat_Prix"].ToString(),
                            PlatNombrePortion = Convert.ToInt32(reader["Plat_Nombre_Portion"]),
                            CuisinierUsername = reader["Cuisinier_Username"].ToString(),
                            RecetteId = Convert.ToInt32(reader["Recette_id"]),
                            PlatDuJour = platDuJour
                        };
                    }
                }
            }
            return plat;
        }
        public void Insert(Plat entity)
        {
            string query = @"INSERT INTO Plat 
                             (Plat_Id, Plat_date_de_fabrication, Plat_Date_de_peremption, Plat_Prix, Plat_Nombre_Portion, Cuisinier_Username, Recette_id, PlatDuJour)
                             VALUES (@Id, @DateFab, @DatePeremption, @Prix, @NombrePortion, @CuisinierUsername, @RecetteId, @PlatDuJour)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.PlatId);
                command.Parameters.AddWithValue("@DateFab", entity.PlatDateDeFabrication);
                command.Parameters.AddWithValue("@DatePeremption", entity.PlatDateDePeremption);
                command.Parameters.AddWithValue("@Prix", entity.PlatPrix);
                command.Parameters.AddWithValue("@NombrePortion", entity.PlatNombrePortion);
                command.Parameters.AddWithValue("@CuisinierUsername", entity.CuisinierUsername);
                command.Parameters.AddWithValue("@RecetteId", entity.RecetteId);
                command.Parameters.AddWithValue("@PlatDuJour", entity.PlatDuJour);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Plat entity)
        {
            string query = @"UPDATE Plat SET 
                             Plat_date_de_fabrication = @DateFab,
                             Plat_Date_de_peremption = @DatePeremption,
                             Plat_Prix = @Prix,
                             Plat_Nombre_Portion = @NombrePortion,
                             Cuisinier_Username = @CuisinierUsername,
                             Recette_id = @RecetteId,
                             PlatDuJour = @PlatDuJour
                             WHERE Plat_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@DateFab", entity.PlatDateDeFabrication);
                command.Parameters.AddWithValue("@DatePeremption", entity.PlatDateDePeremption);
                command.Parameters.AddWithValue("@Prix", entity.PlatPrix);
                command.Parameters.AddWithValue("@NombrePortion", entity.PlatNombrePortion);
                command.Parameters.AddWithValue("@CuisinierUsername", entity.CuisinierUsername);
                command.Parameters.AddWithValue("@RecetteId", entity.RecetteId);
                command.Parameters.AddWithValue("@Id", entity.PlatId);
                command.Parameters.AddWithValue("@PlatDuJour", entity.PlatDuJour);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            string query = "DELETE FROM Plat WHERE Plat_Id = @Id";
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
