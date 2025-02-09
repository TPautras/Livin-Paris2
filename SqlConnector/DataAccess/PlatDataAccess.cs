using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SqlConnector
{
    public class PlatDataAccess
    {
        private readonly Database db = new Database();

        // 🔹 Ajouter un plat
        public void Ajouter(Plat plat)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Plats (nom, description, categorie, personnes, prix, nationalite, regime_alimentaire, ingredients, cuisinier_id) VALUES (@nom, @description, @categorie, @personnes, @prix, @nationalite, @regime, @ingredients, @cuisinier_id)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", plat.Nom);
                cmd.Parameters.AddWithValue("@description", plat.Description);
                cmd.Parameters.AddWithValue("@categorie", plat.Categorie);
                cmd.Parameters.AddWithValue("@personnes", plat.Personnes);
                cmd.Parameters.AddWithValue("@prix", plat.Prix);
                cmd.Parameters.AddWithValue("@nationalite", plat.Nationalite);
                cmd.Parameters.AddWithValue("@regime", plat.RegimeAlimentaire);
                cmd.Parameters.AddWithValue("@ingredients", plat.Ingredients);
                cmd.Parameters.AddWithValue("@cuisinier_id", plat.CuisinierId);
                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 Lire tous les plats
        public List<Plat> ObtenirTous()
        {
            List<Plat> plats = new List<Plat>();

            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Plats";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        plats.Add(new Plat
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("nom"),
                            Description = reader.GetString("description"),
                            Categorie = reader.GetString("categorie"),
                            Personnes = reader.GetInt32("personnes"),
                            Prix = reader.GetDecimal("prix"),
                            Nationalite = reader.GetString("nationalite"),
                            RegimeAlimentaire = reader.GetString("regime_alimentaire"),
                            Ingredients = reader.GetString("ingredients"),
                            CuisinierId = reader.GetInt32("cuisinier_id")
                        });
                    }
                }
            }
            return plats;
        }

        // 🔹 Supprimer un plat
        public void Supprimer(int id)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Plats WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}