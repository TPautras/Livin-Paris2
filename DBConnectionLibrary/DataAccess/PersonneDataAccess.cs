using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using LivinParis.DataAccess;
using SqlConnector.Models;

namespace SqlConnector.DataAccess
{
    public class PersonneDataAccess : BaseDataAccess, IDataAccess<Personne>
    {
        public List<Personne> GetAll()
        {
            var personnes = new List<Personne>();
            string query = "SELECT * FROM Personne";
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        personnes.Add(new Personne
                        {
                            PersonneId = reader["Personne_Id"].ToString(),
                            PersonneNom = reader["Personne_Nom"].ToString(),
                            PersonnePrenom = reader["Personne_Prenom"].ToString(),
                            PersonneVille = reader["Personne_Ville"].ToString(),
                            PersonneCodePostale = Convert.ToInt32(reader["Personne_Code_postale"]),
                            PersonneNomDeLaRue = reader["Personne_Nom_de_la_rue"].ToString(),
                            PersonneNumeroDeLaRue = Convert.ToInt32(reader["Personne_Numero_de_la_rue"]),
                            PersonneEmail = reader["Personne_Email"].ToString(),
                            PersonneTelephone = reader["Personne_Telephone"].ToString(),
                            PersonneStationDeMetroLaPlusProche = reader["Personne_Station_de_metro_la_plus_proche"].ToString()
                        });
                    }
                }
            }
            return personnes;
        }

        // Ici, nous supposons que l'identifiant int correspond à une conversion en string.
        public Personne GetById(int id)
        {
            Personne p = null;
            string query = "SELECT * FROM Personne WHERE Personne_Id = @PersonneId";
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                // Conversion de l'id en chaîne.
                command.Parameters.AddWithValue("@PersonneId", id.ToString());
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        p = new Personne
                        {
                            PersonneId = reader["Personne_Id"].ToString(),
                            PersonneNom = reader["Personne_Nom"].ToString(),
                            PersonnePrenom = reader["Personne_Prenom"].ToString(),
                            PersonneVille = reader["Personne_Ville"].ToString(),
                            PersonneCodePostale = Convert.ToInt32(reader["Personne_Code_postale"]),
                            PersonneNomDeLaRue = reader["Personne_Nom_de_la_rue"].ToString(),
                            PersonneNumeroDeLaRue = Convert.ToInt32(reader["Personne_Numero_de_la_rue"]),
                            PersonneEmail = reader["Personne_Email"].ToString(),
                            PersonneTelephone = reader["Personne_Telephone"].ToString(),
                            PersonneStationDeMetroLaPlusProche = reader["Personne_Station_de_metro_la_plus_proche"].ToString()
                        };
                    }
                }
            }
            return p;
        }

        public void Insert(Personne entity)
        {
            string query = @"INSERT INTO Personne 
                             (Personne_Id, Personne_Nom, Personne_Prenom, Personne_Ville, Personne_Code_postale,
                              Personne_Nom_de_la_rue, Personne_Numero_de_la_rue, Personne_Email, Personne_Telephone, Personne_Station_de_metro_la_plus_proche)
                             VALUES (@PersonneId, @Nom, @Prenom, @Ville, @CodePostale, @NomRue, @NumeroRue, @Email, @Telephone, @Metro)";
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@PersonneId", entity.PersonneId);
                command.Parameters.AddWithValue("@Nom", entity.PersonneNom);
                command.Parameters.AddWithValue("@Prenom", entity.PersonnePrenom);
                command.Parameters.AddWithValue("@Ville", entity.PersonneVille);
                command.Parameters.AddWithValue("@CodePostale", entity.PersonneCodePostale);
                command.Parameters.AddWithValue("@NomRue", entity.PersonneNomDeLaRue);
                command.Parameters.AddWithValue("@NumeroRue", entity.PersonneNumeroDeLaRue);
                command.Parameters.AddWithValue("@Email", entity.PersonneEmail);
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
                             Personne_Email = @Email,
                             Personne_Telephone = @Telephone,
                             Personne_Station_de_metro_la_plus_proche = @Metro
                             WHERE Personne_Id = @PersonneId";
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Nom", entity.PersonneNom);
                command.Parameters.AddWithValue("@Prenom", entity.PersonnePrenom);
                command.Parameters.AddWithValue("@Ville", entity.PersonneVille);
                command.Parameters.AddWithValue("@CodePostale", entity.PersonneCodePostale);
                command.Parameters.AddWithValue("@NomRue", entity.PersonneNomDeLaRue);
                command.Parameters.AddWithValue("@NumeroRue", entity.PersonneNumeroDeLaRue);
                command.Parameters.AddWithValue("@Email", entity.PersonneEmail);
                command.Parameters.AddWithValue("@Telephone", entity.PersonneTelephone);
                command.Parameters.AddWithValue("@Metro", entity.PersonneStationDeMetroLaPlusProche);
                command.Parameters.AddWithValue("@PersonneId", entity.PersonneId);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Personne WHERE Personne_Id = @PersonneId";
            using (var connection = GetConnection())
            using (var command = new SqlCommand(query, connection))
            {
                // Conversion de l'id en chaîne.
                command.Parameters.AddWithValue("@PersonneId", id.ToString());
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
