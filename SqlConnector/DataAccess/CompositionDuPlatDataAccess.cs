using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class CompositionDuPlatDataAccess : IDataAccess<CompositionDuPlat>
    {
        private readonly Database _database;

        public CompositionDuPlatDataAccess()
        {
            _database = new Database();
        }

        public List<CompositionDuPlat> GetAll()
        {
            var result = new List<CompositionDuPlat>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Composition_du_plat";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new CompositionDuPlat
                        {
                            PlatId = reader.GetInt32("Plat_Id"),
                            IngredientId = reader.GetInt32("Ingredient_Id")
                        };
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        public CompositionDuPlat GetById(int id)
        {
            return null;
        }

        public void Insert(CompositionDuPlat entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Composition_du_plat (Plat_Id, Ingredient_Id) VALUES (@plat, @ing)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@plat", entity.PlatId);
                    cmd.Parameters.AddWithValue("@ing", entity.IngredientId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(CompositionDuPlat entity)
        {
        }

        public void Delete(int id)
        {
        }
    }
}