using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace SqlConnector
{
    public class UserDataAccess
    {
        private readonly Database db = new Database();

        // 🔹 Créer un utilisateur
        public void Ajouter(User user)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Utilisateurs (nom, prenom, email, telephone, adresse, type, password) VALUES (@nom, @prenom, @email, @telephone, @adresse, @type, @password)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", user.Nom);
                cmd.Parameters.AddWithValue("@prenom", user.Prenom);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@telephone", user.Telephone);
                cmd.Parameters.AddWithValue("@adresse", user.Adresse);
                cmd.Parameters.AddWithValue("@type", user.Type);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 Lire tous les utilisateurs
        public List<User> ObtenirTous()
        {
            List<User> utilisateurs = new List<User>();

            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Utilisateurs";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        utilisateurs.Add(new User
                        {
                            Id = reader.GetInt32("id"),
                            Nom = reader.GetString("nom"),
                            Prenom = reader.GetString("prenom"),
                            Email = reader.GetString("email"),
                            Telephone = reader.GetString("telephone"),
                            Adresse = reader.GetString("adresse"),
                            Type = reader.GetString("type"),
                            Password = reader.GetString("password")
                        });
                    }
                }
            }
            return utilisateurs;
        }

        // 🔹 Mettre à jour un utilisateur
        public void MettreAJour(User user)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Utilisateurs SET nom=@nom, prenom=@prenom, email=@email, telephone=@telephone, adresse=@adresse, type=@type WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", user.Id);
                cmd.Parameters.AddWithValue("@nom", user.Nom);
                cmd.Parameters.AddWithValue("@prenom", user.Prenom);
                cmd.Parameters.AddWithValue("@email", user.Email);
                cmd.Parameters.AddWithValue("@telephone", user.Telephone);
                cmd.Parameters.AddWithValue("@adresse", user.Adresse);
                cmd.Parameters.AddWithValue("@type", user.Type);
                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 Supprimer un utilisateur
        public void Supprimer(int id)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "DELETE FROM Utilisateurs WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}