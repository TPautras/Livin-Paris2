using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class CompositionDeLaRecetteDataAccess : BaseDataAccess, IDataAccess<CompositionDeLaRecette>
    {
        public List<CompositionDeLaRecette> GetAll()
        {
            var list = new List<CompositionDeLaRecette>();
            string query = "SELECT * FROM Composition_de_la_recette";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new CompositionDeLaRecette
                        {
                            IngredientId = Convert.ToInt32(reader["Ingredient_Id"]),
                            RecetteId = Convert.ToInt32(reader["Recette_id"])
                        });
                    }
                }
            }
            return list;
        }

        public CompositionDeLaRecette GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite."); // si ça lit ça à un moment==> erreur
            //récuperer une seul composition de recette par son id
        }

        public void Insert(CompositionDeLaRecette entity)
        {
            string query = "INSERT INTO Composition_de_la_recette (Ingredient_Id, Recette_id) VALUES (@IngredientId, @RecetteId)";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@IngredientId", entity.IngredientId);
                command.Parameters.AddWithValue("@RecetteId", entity.RecetteId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(CompositionDeLaRecette entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite.");
        }
    }
}
