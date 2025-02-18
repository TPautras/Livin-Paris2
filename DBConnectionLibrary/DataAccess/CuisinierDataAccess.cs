using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class CuisinierDataAccess : BaseDataAccess, IDataAccess<Cuisinier>
    {
        public List<Cuisinier> GetAll()
        {
            var list = new List<Cuisinier>();
            string query = "SELECT * FROM Cuisinier";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Cuisinier
                        {
                            CuisinierId = Convert.ToInt32(reader["Cuisinier_Id"]),
                            CuisinierPassword = reader["Cuisinier_Password"].ToString(),
                            PersonneId = reader["Personne_Id"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Cuisinier GetById(int id)
        {
            Cuisinier cuisinier = null;
            string query = "SELECT * FROM Cuisinier WHERE Cuisinier_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        cuisinier = new Cuisinier
                        {
                            CuisinierId = Convert.ToInt32(reader["Cuisinier_Id"]),
                            CuisinierPassword = reader["Cuisinier_Password"].ToString(),
                            PersonneId = reader["Personne_Id"].ToString()
                        };
                    }
                }
            }
            return cuisinier;
        }

        public void Insert(Cuisinier entity)
        {
            string query = "INSERT INTO Cuisinier (Cuisinier_Id, Cuisinier_Password, Personne_Id) VALUES (@Id, @Password, @PersonneId)";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.CuisinierId);
                command.Parameters.AddWithValue("@Password", entity.CuisinierPassword);
                command.Parameters.AddWithValue("@PersonneId", entity.PersonneId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Cuisinier entity)
        {
            string query = "UPDATE Cuisinier SET Cuisinier_Password = @Password, Personne_Id = @PersonneId WHERE Cuisinier_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", entity.CuisinierPassword);
                command.Parameters.AddWithValue("@PersonneId", entity.PersonneId);
                command.Parameters.AddWithValue("@Id", entity.CuisinierId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Cuisinier WHERE Cuisinier_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
