using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LivinParis.DataAccess;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class CreationDataAccess : BaseDataAccess, IDataAccess<Creation>
    {
        public List<Creation> GetAll()
        {
            var list = new List<Creation>();
            string query = "SELECT * FROM Creation";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Creation
                        {
                            CommandeId = Convert.ToInt32(reader["Commande_Id"]),
                            PlatId = Convert.ToInt32(reader["Plat_Id"]),
                            Quantity = Convert.ToInt32(reader["Creation_Quantity"]),
                        });
                    }
                }
            }
            return list;
        }

        public Creation GetById(int id)
        {
            Creation c = null;
            string query = "SELECT * FROM Creation WHERE Commande_Id = @id OR Plat_Id = @id";
            using(var connection = GetConnection())
            using (var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        c = new Creation
                        {
                            CommandeId =  Convert.ToInt32(reader["Commande_Id"]),
                            PlatId = Convert.ToInt32(reader["Plat_Id"]),
                            Quantity = Convert.ToInt32(reader["Creation_Quantity"]),
                            
                        };
                    }
                }
            }
            return c;
        }

        public void Insert(Creation entity)
        {
            string query = "INSERT INTO Creation (Commande_Id, Plat_Id, Creation_Quantity) VALUES (@CommandeId, @PlatId, @CreationQuantity)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CommandeId", entity.CommandeId);
                command.Parameters.AddWithValue("@PlatId", entity.PlatId);
                command.Parameters.AddWithValue("@CreationQuantity", entity.Quantity);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Creation entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite. Utilisez une méthode dédiée.");
        }
    }
}
