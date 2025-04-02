using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using LivinParis.DataAccess;
using SqlConnector;
using SqlConnector.Models;

namespace DBConnectionLibrary.DataAccess
{
    public class StatsDataAccess : BaseDataAccess
    {
        public Dictionary<string, int> GetLivraisonCountByCuisinier()
        {
            var result = new Dictionary<string, int>();
            string query = @"
                SELECT Cuisinier_Username, COUNT(*) AS LivraisonCount
                FROM livré
                JOIN Plat ON livré.Plat_Id = Plat.Plat_Id
                GROUP BY Cuisinier_Username";

            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result[reader["Cuisinier_Username"].ToString()] = Convert.ToInt32(reader["LivraisonCount"]);
                    }
                }
            }
            return result;
        }

        public List<int> GetCommandesBetweenDates(DateTime start, DateTime end)
        {
            var result = new List<int>();
            string query = @"
                SELECT DISTINCT Commande.Commande_Id
                FROM Commande
                JOIN Creation ON Commande.Commande_Id = Creation.Commande_Id
                JOIN Plat ON Creation.Plat_Id = Plat.Plat_Id
                WHERE Plat.Plat_date_de_fabrication BETWEEN @Start AND @End";

            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Start", start);
                command.Parameters.AddWithValue("@End", end);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToInt32(reader["Commande_Id"]));
                    }
                }
            }
            return result;
        }

        public double GetAverageCommandePrice()
        {
            string query = "SELECT AVG(CAST(Plat_Prix AS DECIMAL(10,2))) FROM Plat";

            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                return Convert.ToDouble(command.ExecuteScalar());
            }
        }

        public double GetAverageClientCount()
        {
            string query = "SELECT COUNT(*) FROM Clients";

            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                return Convert.ToDouble(command.ExecuteScalar());
            }
        }

        public List<int> GetCommandesByClientAndFilters(string clientUsername, string recetteOrigine, DateTime start, DateTime end)
        {
            var result = new List<int>();
            string query = @"
                SELECT DISTINCT Commande.Commande_Id
                FROM Commande
                JOIN Creation ON Commande.Commande_Id = Creation.Commande_Id
                JOIN Plat ON Creation.Plat_Id = Plat.Plat_Id
                JOIN Recette ON Plat.Recette_id = Recette.Recette_id
                WHERE Commande.Client_Username = @Username
                  AND Recette.Recette_Origine = @Origine
                  AND Plat.Plat_date_de_fabrication BETWEEN @Start AND @End";

            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", clientUsername);
                command.Parameters.AddWithValue("@Origine", recetteOrigine);
                command.Parameters.AddWithValue("@Start", start);
                command.Parameters.AddWithValue("@End", end);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(Convert.ToInt32(reader["Commande_Id"]));
                    }
                }
            }
            return result;
        }
    }
}
