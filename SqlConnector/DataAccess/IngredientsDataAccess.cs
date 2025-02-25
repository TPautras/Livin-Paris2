using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class IngredientDataAccess : IDataAccess<Ingredient>
    {
        private readonly Database _database;

        public IngredientDataAccess()
        {
            _database = new Database();
        }

        public List<Ingredient> GetAll()
        {
            var result = new List<Ingredient>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Ingredient";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var i = new Ingredient
                        {
                            Ingredient_Id = reader.GetInt32("Ingredient_Id"),
                            Ingredient_Nom = reader["Ingredient_Nom"] as string,
                            Ingredient_Volume = reader["Ingredient_Volume"] as string
                        };
                        result.Add(i);
                    }
                }
            }
            return result;
        }

        public Ingredient GetById(int id)
        {
            Ingredient i = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Ingredient WHERE Ingredient_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            i = new Ingredient
                            {
                                Ingredient_Id = reader.GetInt32("Ingredient_Id"),
                                Ingredient_Nom = reader["Ingredient_Nom"] as string,
                                Ingredient_Volume = reader["Ingredient_Volume"] as string
                            };
                        }
                    }
                }
            }
            return i;
        }

        public void Insert(Ingredient entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Ingredient (Ingredient_Id, Ingredient_Nom, Ingredient_Volume) 
                              VALUES (@id, @nom, @vol)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.Ingredient_Id);
                    cmd.Parameters.AddWithValue("@nom", (object)entity.Ingredient_Nom ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@vol", (object)entity.Ingredient_Volume ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Ingredient entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Ingredient 
                              SET Ingredient_Nom = @nom, Ingredient_Volume = @vol
                              WHERE Ingredient_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.Ingredient_Id);
                    cmd.Parameters.AddWithValue("@nom", (object)entity.Ingredient_Nom ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@vol", (object)entity.Ingredient_Volume ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Ingredient WHERE Ingredient_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}