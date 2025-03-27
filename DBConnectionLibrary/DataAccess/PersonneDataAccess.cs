using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using MySql.Data.MySqlClient;
using SqlConnector.Models;
namespace SqlConnector.DataAccess
{
    public class PersonneDataAccess : BaseDataAccess, IDataAccess<Personne>
    {
        public List<Personne> GetAll()
        {
            var list = new List<Personne>();
            string query = "SELECT * FROM Personne";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        list.Add(new Personne
                        {
                            PersonneEmail = reader["Personne_Email"].ToString(),
                            PersonneNom = reader["Personne_Nom"].ToString(),
                            PersonnePrenom = reader["Personne_Prenom"].ToString(),
                            PersonneVille = reader["Personne_Ville"].ToString(),
                            PersonneCodePostale = Convert.ToInt32(reader["Personne_Code_postale"]),
                            PersonneNomDeLaRue = reader["Personne_Nom_de_la_rue"].ToString(),
                            PersonneNumeroDeLaRue = Convert.ToInt32(reader["Personne_Numero_de_la_rue"]),
                            PersonneTelephone = reader["Personne_Telephone"].ToString(),
                            PersonneStationDeMetroLaPlusProche = reader["Personne_Station_de_metro_la_plus_proche"].ToString()
                        });
                    }
                }
            }
            return list;
        }
        public Personne GetById(int id)
        {
            throw new NotImplementedException("Utilisez GetByEmail pour récupérer une Personne.");
        }
        public void Insert(Personne entity)
        {
            string query = @"INSERT INTO Personne 
                             (Personne_Email, Personne_Nom, Personne_Prenom, Personne_Ville, Personne_Code_postale,
                              Personne_Nom_de_la_rue, Personne_Numero_de_la_rue, Personne_Telephone, Personne_Station_de_metro_la_plus_proche)
                             VALUES (@Email, @Nom, @Prenom, @Ville, @CodePostale, @NomRue, @NumeroRue, @Telephone, @Metro)";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Nom", entity.PersonneNom);
                command.Parameters.AddWithValue("@Prenom", entity.PersonnePrenom);
                command.Parameters.AddWithValue("@Ville", entity.PersonneVille);
                command.Parameters.AddWithValue("@CodePostale", entity.PersonneCodePostale);
                command.Parameters.AddWithValue("@NomRue", entity.PersonneNomDeLaRue);
                command.Parameters.AddWithValue("@NumeroRue", entity.PersonneNumeroDeLaRue);
                command.Parameters.AddWithValue("@Telephone", entity.PersonneTelephone);
                command.Parameters.AddWithValue("@Metro", entity.PersonneStationDeMetroLaPlusProche);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Update(Personne entity)
        {
            string query = @"UPDATE Personne SET 
                             Personne_Nom = @Nom,
                             Personne_Prenom = @Prenom,
                             Personne_Ville = @Ville,
                             Personne_Code_postale = @CodePostale,
                             Personne_Nom_de_la_rue = @NomRue,
                             Personne_Numero_de_la_rue = @NumeroRue,
                             Personne_Telephone = @Telephone,
                             Personne_Station_de_metro_la_plus_proche = @Metro
                             WHERE Personne_Email = @Email";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nom", entity.PersonneNom);
                command.Parameters.AddWithValue("@Prenom", entity.PersonnePrenom);
                command.Parameters.AddWithValue("@Ville", entity.PersonneVille);
                command.Parameters.AddWithValue("@CodePostale", entity.PersonneCodePostale);
                command.Parameters.AddWithValue("@NomRue", entity.PersonneNomDeLaRue);
                command.Parameters.AddWithValue("@NumeroRue", entity.PersonneNumeroDeLaRue);
                command.Parameters.AddWithValue("@Telephone", entity.PersonneTelephone);
                command.Parameters.AddWithValue("@Metro", entity.PersonneStationDeMetroLaPlusProche);
                command.Parameters.AddWithValue("@Email", entity.PersonneEmail);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
        public void Delete(int id)
        {
            throw new NotImplementedException("Utilisez DeleteByEmail pour supprimer une Personne.");
        }
        public Personne GetByEmail(string email)
        {
            Personne p = null;
            string query = "SELECT * FROM Personne WHERE Personne_Email = @Email";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                using(var reader = command.ExecuteReader())
                {
                    if(reader.Read())
                    {
                        p = new Personne
                        {
                            PersonneEmail = reader["Personne_Email"].ToString(),
                            PersonneNom = reader["Personne_Nom"].ToString(),
                            PersonnePrenom = reader["Personne_Prenom"].ToString(),
                            PersonneVille = reader["Personne_Ville"].ToString(),
                            PersonneCodePostale = Convert.ToInt32(reader["Personne_Code_postale"]),
                            PersonneNomDeLaRue = reader["Personne_Nom_de_la_rue"].ToString(),
                            PersonneNumeroDeLaRue = Convert.ToInt32(reader["Personne_Numero_de_la_rue"]),
                            PersonneTelephone = reader["Personne_Telephone"].ToString(),
                            PersonneStationDeMetroLaPlusProche = reader["Personne_Station_de_metro_la_plus_proche"].ToString()
                        };
                    }
                }
            }
            return p;
        }
        public void DeleteByEmail(string email)
        {
            string query = "DELETE FROM Personne WHERE Personne_Email = @Email";
            using(var connection = GetConnection())
            using(var command = new MySqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Email", email);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
