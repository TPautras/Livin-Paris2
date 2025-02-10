using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class PersonneService
    {
        private readonly PersonneDataAccess _dataAccess = new PersonneDataAccess();

        public List<Personne> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Personne GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Personne p)
        {
            ValidatePersonne(p);
            _dataAccess.Insert(p);
        }

        public void Update(Personne p)
        {
            ValidatePersonne(p);
            _dataAccess.Update(p);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }

        private void ValidatePersonne(Personne p)
        {
            ValidationHelper.ValidateStringField(p.PersonneNom, "Personne_Nom", 50, allowNull: false);
            ValidationHelper.ValidateStringField(p.PersonnePrenom, "Personne_Prenom", 50, allowNull: false);
            ValidationHelper.ValidateStringField(p.PersonneNumeroDeLicence, "Personne_Numero_de_licence", 50);
            ValidationHelper.ValidateStringField(p.PersonneVille, "Personne_Ville", 50);
            ValidationHelper.ValidateStringField(p.PersonneCodepostale, "Personne_Code_postale", 10);
            ValidationHelper.ValidateStringField(p.PersonneNomDeLaRue, "Personne_Nom_de_la_rue", 100);
            ValidationHelper.ValidateStringField(p.PersonneEmail, "Personne_Email", 100);
            ValidationHelper.ValidateStringField(p.PersonneTelephone, "Personne_Telephone", 20);
            ValidationHelper.ValidateStringField(p.PersonneStationDeMetroLaPlusProche, 
                "Personne_Station_de_metro_la_plus_proche", 50);
        }
    }
}