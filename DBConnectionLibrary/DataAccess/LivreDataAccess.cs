using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class LivreDataAccess : BaseDataAccess, IDataAccess<Livre>
    {
        public List<Livre> GetAll()
        {
            var list = new List<Livre>();
            string query = "SELECT * FROM livré";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Livre
                        {
                            PlatId = Convert.ToInt32(reader["Plat_Id"]),
                            LivraisonId = Convert.ToInt32(reader["Livraison_Id"])
                        });
                    }
                }
            }
            return list;
        }

        public Livre GetById(int id)
        {
            Livre l = null;
            string query = "SELECT * FROM livré WHERE Livraison_Id = @id OR Plat_Id = @id";
            using(var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        l = new Livre
                        {
                            LivraisonId = Convert.ToInt32(reader["Livraison_Id"]),
                            PlatId = Convert.ToInt32(reader["Plat_Id"]),
                            
                        };
                    }
                }
            }
            return l;
        }

        public void Insert(Livre entity)
        {
            string query = "INSERT INTO livré (Plat_Id, Livraison_Id) VALUES (@PlatId, @LivraisonId)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PlatId", entity.PlatId);
                command.Parameters.AddWithValue("@LivraisonId", entity.LivraisonId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Livre entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite.");
        }
    }
}
