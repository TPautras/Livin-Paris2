using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class CommandeDataAccess
    {
        private readonly Database db = new Database();

        // 🔹 Ajouter une commande
        public void Ajouter(Commande commande)
        {
            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Commandes (client_id, date_commande, prix_total, statut) VALUES (@client_id, @date_commande, @prix_total, @statut)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@client_id", commande.ClientId);
                cmd.Parameters.AddWithValue("@date_commande", commande.DateCommande);
                cmd.Parameters.AddWithValue("@prix_total", commande.PrixTotal);
                cmd.Parameters.AddWithValue("@statut", commande.Statut);
                cmd.ExecuteNonQuery();
            }
        }

        // 🔹 Lire toutes les commandes
        public List<Commande> ObtenirToutes()
        {
            List<Commande> commandes = new List<Commande>();

            using (var conn = db.GetConnection())
            {
                conn.Open();
                string query = "SELECT * FROM Commandes";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        commandes.Add(new Commande
                        {
                            Id = reader.GetInt32("id"),
                            ClientId = reader.GetInt32("client_id"),
                            DateCommande = reader.GetDateTime("date_commande"),
                            PrixTotal = reader.GetDecimal("prix_total"),
                            Statut = reader.GetString("statut")
                        });
                    }
                }
            }
            return commandes;
        }
    }
}
