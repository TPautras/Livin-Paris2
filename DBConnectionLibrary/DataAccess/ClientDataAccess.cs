using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;
namespace SqlConnector.DataAccess
{
    public class ClientDataAccess : BaseDataAccess, IDataAccess<Client>
    {
        public List<Client> GetAll()
        {
            var list = new List<Client>();
            string query = "SELECT * FROM Clients";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public Client GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Client GetByUsername(string username)
        {
            Client c = null;
            string query = "SELECT * FROM Clients WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        c = new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
                        };
                    }
                }
            }
            return c;
        }
        public Client GetByEmail(string email)
        {
            Client c = null;
            Console.WriteLine(email+"DataAccess");
            string query = "SELECT * FROM Client WHERE Personne_Email = @email";
    
            try
            {
                using (var connection = GetConnection())
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", email.Trim());
                    connection.Open();

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            c = new Client
                            {
                                ClientUsername = reader["Client_Username"]?.ToString(),
                                ClientPassword = reader["Client_Password"]?.ToString(),
                                PersonneEmail = reader["Personne_Email"]?.ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur SQL : " + ex.Message);
            }

            return c;
        }
        public void Insert(Client entity)
        {
            string query = @"INSERT INTO Clients 
                             (Client_Username, Client_Password, Personne_Email)
                             VALUES (@Username, @Password, @PersonneEmail)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                command.Parameters.AddWithValue("@Password", entity.ClientPassword);
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Client entity)
        {
            string query = @"UPDATE Clients SET 
                             Client_Password = @Password,
                             Personne_Email = @PersonneEmail
                             WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", entity.ClientPassword);
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void UpdateUsername(Client entity)
        {
            string query = @"UPDATE Clients SET 
                             Client_Username = @Username,
                             Personne_Email = @PersonneEmail
                             WHERE Client_Password = @Password";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Password", entity.ClientPassword);
                command.Parameters.AddWithValue("@PersonneEmail", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Username", entity.ClientUsername);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteByUsername(string username)
        {
            string query = "DELETE FROM Clients WHERE Client_Username = @Username";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Username", username);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public List<Client> GetAllByNameAsc()
        {
            var list = new List<Client>();
            string query = @"
                SELECT c.Client_Username, c.Client_Password, c.Personne_Email
                FROM Clients c
                JOIN Personne p ON c.Personne_Email = p.Personne_Email
                ORDER BY p.Personne_Nom, p.Personne_Prenom";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<Client> GetAllByStreetAsc()
        {
            var list = new List<Client>();
            string query = @"
                SELECT c.Client_Username, c.Client_Password, c.Personne_Email
                FROM Clients c
                JOIN Personne p ON c.Personne_Email = p.Personne_Email
                ORDER BY p.Personne_Nom_de_la_rue";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
                        });
                    }
                }
            }
            return list;
        }

        public List<Client> GetAllByTotalPurchasesDesc()
        {
            var list = new List<Client>();
            string query = @"
                SELECT c.Client_Username, c.Client_Password, c.Personne_Email,
                       IFNULL(SUM(CAST(p.Plat_Prix AS DECIMAL(10,2))), 0) AS TotalSpent
                FROM Clients c
                LEFT JOIN Commande co ON c.Client_Username = co.Client_Username
                LEFT JOIN Creation cr ON co.Commande_Id = cr.Commande_Id
                LEFT JOIN Plat p ON cr.Plat_Id = p.Plat_Id
                GROUP BY c.Client_Username
                ORDER BY TotalSpent DESC";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Client
                        {
                            ClientUsername = reader["Client_Username"].ToString(),
                            ClientPassword = reader["Client_Password"].ToString(),
                            PersonneEmail = reader["Personne_Email"].ToString()
                        });
                    }
                }
            }
            return list;
        }
    }
}
