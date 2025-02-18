﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            using(var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Creation
                        {
                            CommandeId = Convert.ToInt32(reader["Commande_Id"]),
                            PlatId = Convert.ToInt32(reader["Plat_Id"])
                        });
                    }
                }
            }
            return list;
        }

        public Creation GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite. Utilisez une méthode dédiée.");
        }

        public void Insert(Creation entity)
        {
            string query = "INSERT INTO Creation (Commande_Id, Plat_Id) VALUES (@CommandeId, @PlatId)";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@CommandeId", entity.CommandeId);
                command.Parameters.AddWithValue("@PlatId", entity.PlatId);
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
