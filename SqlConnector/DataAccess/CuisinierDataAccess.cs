using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class CuisinierDataAccess : IDataAccess<Cuisinier>
    {
        private readonly Database _database = new Database();

        public List<Cuisinier> GetAll()
        {
            var result = new List<Cuisinier>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Cuisinier";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var c = new Cuisinier
                        {
                            PersonneId = reader.GetInt32("Cuisinier_Id")
                        };
                        result.Add(c);
                    }
                }
            }
            return result;
        }

        public Cuisinier GetById(int id)
        {
            Cuisinier c = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Cuisinier WHERE Cuisinier_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            c = new Cuisinier
                            {
                                PersonneId = reader.GetInt32("Cuisinier_Id")
                            };
                        }
                    }
                }
            }
            return c;
        }

        public void Insert(Cuisinier entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "INSERT INTO Cuisinier (Cuisinier_Id) VALUES (@id)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.PersonneId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Cuisinier entity)
        {
            Console.WriteLine("WORKINPROGRESS");
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Cuisinier WHERE Cuisinier_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}