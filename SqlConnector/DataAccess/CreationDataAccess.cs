using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class CreationDataAccess : IDataAccess<Creation>
    {
        private readonly Database _database= new Database();

        public List<Creation> GetAll()
        {
            var result = new List<Creation>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Creation";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new Creation
                        {
                            CuisinierId = reader.GetInt32("Cuisinier_Id"),
                            PlatId = reader.GetInt32("Plat_Id")
                        };
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        public Creation GetById(int id)
        {
            // Not really relevant because composite key is (Cuisinier_Id, Plat_Id)
            // We'll just fetch based on a single key for demonstration.
            // You might need a different approach for composite PK.
            return null;
        }

        public void Insert(Creation entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Creation (Cuisinier_Id, Plat_Id) VALUES (@cuisinier, @plat)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cuisinier", entity.CuisinierId);
                    cmd.Parameters.AddWithValue("@plat", entity.PlatId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Creation entity)
        {
            // Typically, you'd need logic to handle the composite key update
        }

        public void Delete(int id)
        {
            // Same note about composite key. This is only an example.
        }
    }

}