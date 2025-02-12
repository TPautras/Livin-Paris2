using SqlConnector.Models;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace SqlConnector
{
    public class PersonneDataAccess : IDataAccess<Personne>
    {
        private readonly Database _database = new Database();

        public List<Personne> GetAll()
        {
            var result = new List<Personne>();
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Personne";
                using (var cmd = new MySqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var p = new Personne
                        {
                            PersonneId = reader.GetInt32("Personne_Id"),
                            PersonneNom = reader.GetString("Personne_Nom"),
                            PersonnePrenom = reader.GetString("Personne_Prenom"),
                            PersonneNumeroDeLicence = reader["Personne_Numero_de_licence"] as string,
                            PersonneVille = reader["Personne_Ville"] as string,
                            PersonneCodepostale = reader["Personne_Code_postale"] as string,
                            PersonneNomDeLaRue = reader["Personne_Nom_de_la_rue"] as string,
                            PersonneEmail = reader["Personne_Email"] as string,
                            PersonneTelephone = reader["Personne_Telephone"] as string,
                            PersonneStationDeMetroLaPlusProche = reader["Personne_Station_de_metro_la_plus_proche"] as string
                        };
                        result.Add(p);
                    }
                }
            }
            return result;
        }

        public Personne GetById(int id)
        {
            Personne p = null;
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "SELECT * FROM Personne WHERE Personne_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            p = new Personne
                            {
                                PersonneId = reader.GetInt32("Personne_Id"),
                                PersonneNom = reader.GetString("Personne_Nom"),
                                PersonnePrenom = reader.GetString("Personne_Prenom"),
                                PersonneNumeroDeLicence = reader["Personne_Numero_de_licence"] as string,
                                PersonneVille = reader["Personne_Ville"] as string,
                                PersonneCodepostale = reader["Personne_Code_postale"] as string,
                                PersonneNomDeLaRue = reader["Personne_Nom_de_la_rue"] as string,
                                PersonneEmail = reader["Personne_Email"] as string,
                                PersonneTelephone = reader["Personne_Telephone"] as string,
                                PersonneStationDeMetroLaPlusProche = reader["Personne_Station_de_metro_la_plus_proche"] as string
                            };
                        }
                    }
                }
            }
            return p;
        }

        public void Insert(Personne entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"INSERT INTO Personne 
                    (Personne_Id, Personne_Nom, Personne_Prenom, Personne_Numero_de_licence, Personne_Ville, 
                     Personne_Code_postale, Personne_Nom_de_la_rue, Personne_Email, Personne_Telephone, 
                     Personne_Station_de_metro_la_plus_proche)
                    VALUES
                    (@id, @nom, @prenom, @licence, @ville, @cp, @rue, @mail, @tel, @metro)";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.PersonneId);
                    cmd.Parameters.AddWithValue("@nom", entity.PersonneNom);
                    cmd.Parameters.AddWithValue("@prenom", entity.PersonnePrenom);
                    cmd.Parameters.AddWithValue("@licence", (object)entity.PersonneNumeroDeLicence ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ville", (object)entity.PersonneVille ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cp", (object)entity.PersonneCodepostale ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@rue", (object)entity.PersonneNomDeLaRue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@mail", (object)entity.PersonneEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tel", (object)entity.PersonneTelephone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@metro", (object)entity.PersonneStationDeMetroLaPlusProche ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Update(Personne entity)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = @"UPDATE Personne SET
                    Personne_Nom = @nom,
                    Personne_Prenom = @prenom,
                    Personne_Numero_de_licence = @licence,
                    Personne_Ville = @ville,
                    Personne_Code_postale = @cp,
                    Personne_Nom_de_la_rue = @rue,
                    Personne_Email = @mail,
                    Personne_Telephone = @tel,
                    Personne_Station_de_metro_la_plus_proche = @metro
                    WHERE Personne_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", entity.PersonneId);
                    cmd.Parameters.AddWithValue("@nom", entity.PersonneNom);
                    cmd.Parameters.AddWithValue("@prenom", entity.PersonnePrenom);
                    cmd.Parameters.AddWithValue("@licence", (object)entity.PersonneNumeroDeLicence ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ville", (object)entity.PersonneVille ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@cp", (object)entity.PersonneCodepostale ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@rue", (object)entity.PersonneNomDeLaRue ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@mail", (object)entity.PersonneEmail ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@tel", (object)entity.PersonneTelephone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@metro", (object)entity.PersonneStationDeMetroLaPlusProche ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = _database.GetConnection())
            {
                conn.Open();
                var query = "DELETE FROM Personne WHERE Personne_Id = @id";
                using (var cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}