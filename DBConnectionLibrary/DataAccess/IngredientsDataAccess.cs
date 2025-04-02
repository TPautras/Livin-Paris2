using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class IngredientDataAccess : BaseDataAccess, IDataAccess<Ingredient>
    {
        public List<Ingredient> GetAll()
        {
            var list = new List<Ingredient>();
            string query = "SELECT * FROM Ingredient";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Ingredient
                        {
                            IngredientId = Convert.ToInt32(reader["Ingredient_Id"]),
                            IngredientNom = reader["Ingredient_Nom"].ToString(),
                            IngredientVolume = reader["Ingredient_volume"].ToString(),
                            IngredientUnite = reader["Ingrédient_Unité"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Ingredient GetById(int id)
        {
            Ingredient ingredient = null;
            string query = "SELECT * FROM Ingredient WHERE Ingredient_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ingredient = new Ingredient
                        {
                            IngredientId = Convert.ToInt32(reader["Ingredient_Id"]),
                            IngredientNom = reader["Ingredient_Nom"].ToString(),
                            IngredientVolume = reader["Ingredient_volume"].ToString(),
                            IngredientUnite = reader["Ingrédient_Unité"].ToString()
                        };
                    }
                }
            }
            return ingredient;
        }

        public void Insert(Ingredient entity)
        {
            string query = "INSERT INTO Ingredient (Ingredient_Id, Ingredient_Nom, Ingredient_volume, Ingrédient_Unité) " +
                           "VALUES (@Id, @Nom, @Volume, @Unite)";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.IngredientId);
                command.Parameters.AddWithValue("@Nom", entity.IngredientNom);
                command.Parameters.AddWithValue("@Volume", entity.IngredientVolume);
                command.Parameters.AddWithValue("@Unite", entity.IngredientUnite);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Ingredient entity)
        {
            string query = "UPDATE Ingredient SET Ingredient_Nom = @Nom, Ingredient_volume = @Volume, Ingrédient_Unité = @Unite " +
                           "WHERE Ingredient_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nom", entity.IngredientNom);
                command.Parameters.AddWithValue("@Volume", entity.IngredientVolume);
                command.Parameters.AddWithValue("@Unite", entity.IngredientUnite);
                command.Parameters.AddWithValue("@Id", entity.IngredientId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Ingredient WHERE Ingredient_Id = @Id";
            using (var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
