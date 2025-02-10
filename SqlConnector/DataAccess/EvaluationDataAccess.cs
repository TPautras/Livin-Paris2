using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class EvaluationDataAccess : IDataAccess<Evaluation>
    {
        private readonly Database _database = new Database();

        public List<Evaluation> GetAll()
        {
            var result = new List<Evaluation>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Evaluation";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var e = new Evaluation
                        {
                            EvaluationId = reader.GetInt32("Evaluation_Id"),
                            EvaluationClient = reader["Evaluation_Client"] == DBNull.Value
                                ? null
                                : (int?)reader.GetInt32("Evaluation_Client"),
                            EvaluationCuisinier = reader["Evaluation_Cuisinier"] == DBNull.Value
                                ? null
                                : (int?)reader.GetInt32("Evaluation_Cuisinier"),
                            CommandeId = reader["Commande_Id"] == DBNull.Value
                                ? null
                                : (int?)reader.GetInt32("Commande_Id")
                        };
                        result.Add(e);
                    }
                }
            }
            return result;
        }

        public Evaluation GetById(int id)
        {
            Evaluation e = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Evaluation WHERE Evaluation_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            e = new Evaluation
                            {
                                EvaluationId = reader.GetInt32("Evaluation_Id"),
                                EvaluationClient = reader["Evaluation_Client"] == DBNull.Value
                                    ? null
                                    : (int?)reader.GetInt32("Evaluation_Client"),
                                EvaluationCuisinier = reader["Evaluation_Cuisinier"] == DBNull.Value
                                    ? null
                                    : (int?)reader.GetInt32("Evaluation_Cuisinier"),
                                CommandeId = reader["Commande_Id"] == DBNull.Value
                                    ? null
                                    : (int?)reader.GetInt32("Commande_Id")
                            };
                        }
                    }
                }
            }
            return e;
        }

        public void Insert(Evaluation entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Evaluation 
                    (Evaluation_Id, Evaluation_Client, Evaluation_Cuisinier, Commande_Id) 
                    VALUES (@id, @client, @cuisinier, @commande)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.EvaluationId);
                    cmd.Parameters.AddWithValue("@client", (object)entity.EvaluationClient ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cuisinier", (object)entity.EvaluationCuisinier ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@commande", (object)entity.CommandeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Evaluation entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Evaluation SET 
                    Evaluation_Client = @client, 
                    Evaluation_Cuisinier = @cuisinier, 
                    Commande_Id = @commande
                    WHERE Evaluation_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.EvaluationId);
                    cmd.Parameters.AddWithValue("@client", (object)entity.EvaluationClient ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cuisinier", (object)entity.EvaluationCuisinier ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@commande", (object)entity.CommandeId ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Evaluation WHERE Evaluation_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}