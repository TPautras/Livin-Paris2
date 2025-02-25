using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class PlatDataAccess : IDataAccess<Plat>
    {
        private readonly Database _database = new Database();

        public List<Plat> GetAll()
        {
            var result = new List<Plat>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Plat";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Plat
                        {
                            PlatId = reader.GetInt32("Plat_Id"),
                            PlatNom = reader["Plat_Nom"] as string,
                            PlatOrigine = reader["Plat_Origine"] as string,
                            PlatAromesNaturels = reader["Plat_Aromes_naturels"] as string,
                            PlatDateDeFabrication = reader["Plat_Date_de_fabrication"] == DBNull.Value 
                                ? (DateTime?)null 
                                : reader.GetDateTime("Plat_Date_de_fabrication"),
                            PlatDateDePeremption = reader["Plat_Date_de_peremption"] == DBNull.Value 
                                ? (DateTime?)null 
                                : reader.GetDateTime("Plat_Date_de_peremption"),
                            PlatTypeDePlat = reader["Plat_Type_de_plat"] as string,
                            PlatRegimeAlimentaire = reader["Plat_Regime_alimentaire"] as string
                        };
                        result.Add(p);
                    }
                }
            }
            return result;
        }

        public Plat GetById(int id)
        {
            Plat p = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Plat WHERE Plat_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = new Plat
                            {
                                PlatId = reader.GetInt32("Plat_Id"),
                                PlatNom = reader["Plat_Nom"] as string,
                                PlatOrigine = reader["Plat_Origine"] as string,
                                PlatAromesNaturels = reader["Plat_Aromes_naturels"] as string,
                                PlatDateDeFabrication = reader["Plat_Date_de_fabrication"] == DBNull.Value 
                                    ? (DateTime?)null 
                                    : reader.GetDateTime("Plat_Date_de_fabrication"),
                                PlatDateDePeremption = reader["Plat_Date_de_peremption"] == DBNull.Value 
                                    ? (DateTime?)null 
                                    : reader.GetDateTime("Plat_Date_de_peremption"),
                                PlatTypeDePlat = reader["Plat_Type_de_plat"] as string,
                                PlatRegimeAlimentaire = reader["Plat_Regime_alimentaire"] as string
                            };
                        }
                    }
                }
            }
            return p;
        }

        public void Insert(Plat entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Plat
                    (Plat_Id, Plat_Nom, Plat_Origine, Plat_Aromes_naturels, Plat_Date_de_fabrication, 
                     Plat_Date_de_peremption, Plat_Type_de_plat, Plat_Regime_alimentaire)
                     VALUES (@id, @nom, @origine, @aromes, @fab, @peremp, @type, @regime)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.PlatId);
                    cmd.Parameters.AddWithValue("@nom", (object)entity.PlatNom ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@origine", (object)entity.PlatOrigine ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@aromes", (object)entity.PlatAromesNaturels ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fab", (object)entity.PlatDateDeFabrication ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@peremp", (object)entity.PlatDateDePeremption ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@type", (object)entity.PlatTypeDePlat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@regime", (object)entity.PlatRegimeAlimentaire ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Plat entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Plat SET
                    Plat_Nom = @nom,
                    Plat_Origine = @origine,
                    Plat_Aromes_naturels = @aromes,
                    Plat_Date_de_fabrication = @fab,
                    Plat_Date_de_peremption = @peremp,
                    Plat_Type_de_plat = @type,
                    Plat_Regime_alimentaire = @regime
                    WHERE Plat_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.PlatId);
                    cmd.Parameters.AddWithValue("@nom", (object)entity.PlatNom ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@origine", (object)entity.PlatOrigine ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@aromes", (object)entity.PlatAromesNaturels ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@fab", (object)entity.PlatDateDeFabrication ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@peremp", (object)entity.PlatDateDePeremption ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@type", (object)entity.PlatTypeDePlat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@regime", (object)entity.PlatRegimeAlimentaire ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Plat WHERE Plat_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}