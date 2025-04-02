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
                SELECT cuisinier_Username, COUNT(*) AS LivraisonCount
                FROM livré
                JOIN plat ON livré.Plat_Id = plat.Plat_Id
                GROUP BY cuisinier_Username";

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
                SELECT DISTINCT commande.Commande_Id
                FROM commande
                WHERE Commande_Date BETWEEN @Start AND @End";

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
            string query = "SELECT AVG(TotalPrice) FROM (SELECT Commande_Id, SUM(CAST(Plat_Prix AS DECIMAL(10,2))) AS TotalPrice FROM Creation INNER JOIN Plat ON Creation.Plat_Id = Plat.Plat_Id GROUP BY Commande_Id) AS OrderPrices";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                return Convert.ToDouble(command.ExecuteScalar());
            }
        }

        public double GetAverageClientSpending()
        {
            string query = "SELECT AVG(ClientTotal) FROM (SELECT c.Client_Username, SUM(CAST(p.Plat_Prix AS DECIMAL(10,2))) AS ClientTotal FROM Clients c JOIN Commande co ON c.Client_Username = co.Client_Username JOIN Creation cr ON co.Commande_Id = cr.Commande_Id JOIN Plat p ON cr.Plat_Id = p.Plat_Id GROUP BY c.Client_Username) AS ClientSpending";
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
        SELECT DISTINCT commande.Commande_Id
        FROM commande
        JOIN creation ON commande.Commande_Id = creation.Commande_Id
        JOIN plat ON creation.Plat_Id = plat.Plat_Id
        JOIN recette ON plat.Recette_id = recette.Recette_id
        WHERE commande.Client_Username = @Username
          AND recette.Recette_Origine = @Origine
          AND plat.Plat_date_de_fabrication BETWEEN @Start AND @End";
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
