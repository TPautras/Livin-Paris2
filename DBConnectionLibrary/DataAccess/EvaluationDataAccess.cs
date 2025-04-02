using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class EvaluationDataAccess : BaseDataAccess, IDataAccess<Evaluation>
    {
        public List<Evaluation> GetAll()
        {
            var list = new List<Evaluation>();
            string query = "SELECT * FROM evaluation";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Evaluation
                        {
                            EvaluationId = Convert.ToInt32(reader["Evaluation_Id"]),
                            EvaluationClient = Convert.ToDecimal(reader["Evaluation_Client"]),
                            EvaluationCuisinier = Convert.ToDecimal(reader["Evaluation_Cuisinier"]),
                            EvaluationDescriptionClient = reader["Evaluation_Description_Client"].ToString(),
                            EvaluationDescriptionCuisinier = reader["Evaluation_Description_Cuisinier"].ToString(),
                            CommandeId = Convert.ToInt32(reader["Commande_Id"])
                        });
                    }
                }
            }
            return list;
        }

        public Evaluation GetById(int id)
        {
            Evaluation evaluation = null;
            string query = "SELECT * FROM evaluation WHERE Evaluation_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        evaluation = new Evaluation
                        {
                            EvaluationId = Convert.ToInt32(reader["Evaluation_Id"]),
                            EvaluationClient = Convert.ToDecimal(reader["Evaluation_Client"]),
                            EvaluationCuisinier = Convert.ToDecimal(reader["Evaluation_Cuisinier"]),
                            EvaluationDescriptionClient = reader["Evaluation_Description_Client"].ToString(),
                            EvaluationDescriptionCuisinier = reader["Evaluation_Description_Cuisinier"].ToString(),
                            CommandeId = Convert.ToInt32(reader["Commande_Id"])
                        };
                    }
                }
            }
            return evaluation;
        }

        public void Insert(Evaluation entity)
        {
            string query = @"INSERT INTO evaluation 
                             (Evaluation_Id, Evaluation_Client, Evaluation_Cuisinier, Evaluation_Description_Client, Evaluation_Description_Cuisinier, Commande_Id)
                             VALUES (@Id, @Client, @Cuisinier, @DescClient, @DescCuisinier, @CommandeId)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", entity.EvaluationId);
                command.Parameters.AddWithValue("@Client", entity.EvaluationClient);
                command.Parameters.AddWithValue("@Cuisinier", entity.EvaluationCuisinier);
                command.Parameters.AddWithValue("@DescClient", entity.EvaluationDescriptionClient);
                command.Parameters.AddWithValue("@DescCuisinier", entity.EvaluationDescriptionCuisinier);
                command.Parameters.AddWithValue("@CommandeId", entity.CommandeId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Update(Evaluation entity)
        {
            string query = @"UPDATE evaluation SET 
                             Evaluation_Client = @Client, 
                             Evaluation_Cuisinier = @Cuisinier, 
                             Evaluation_Description_Client = @DescClient, 
                             Evaluation_Description_Cuisinier = @DescCuisinier, 
                             Commande_Id = @CommandeId
                             WHERE Evaluation_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Client", entity.EvaluationClient);
                command.Parameters.AddWithValue("@Cuisinier", entity.EvaluationCuisinier);
                command.Parameters.AddWithValue("@DescClient", entity.EvaluationDescriptionClient);
                command.Parameters.AddWithValue("@DescCuisinier", entity.EvaluationDescriptionCuisinier);
                command.Parameters.AddWithValue("@CommandeId", entity.CommandeId);
                command.Parameters.AddWithValue("@Id", entity.EvaluationId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM evaluation WHERE Evaluation_Id = @Id";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
