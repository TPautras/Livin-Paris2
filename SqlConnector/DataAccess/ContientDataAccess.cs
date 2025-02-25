using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class ContientDataAccess : IDataAccess<Contient>
    {
        private readonly Database _database = new Database();

        public List<Contient> GetAll()
        {
            var result = new List<Contient>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Contient";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new Contient
                        {
                            CommandeId = reader.GetInt32("Commande_Id"),
                            PlatId = reader.GetInt32("Plat_Id")
                        };
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        public Contient GetById(int id)
        {
            return null;
        }

        public void Insert(Contient entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Contient (Commande_Id, Plat_Id) VALUES (@commande, @plat)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@commande", entity.CommandeId);
                    cmd.Parameters.AddWithValue("@plat", entity.PlatId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Contient entity)
        {
        }

        public void Delete(int id)
        {
        }
    }
}