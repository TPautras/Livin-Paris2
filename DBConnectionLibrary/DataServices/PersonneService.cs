// File: PersonneService.cs
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class PersonneService : IDataService<Personne>
    {
        private readonly IDataAccess<Personne> _dataAccess;
        public PersonneService(IDataAccess<Personne> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<Personne> GetAll()
        {
            return _dataAccess.GetAll();
        }
        public Personne GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public Personne GetByEmail(string email)
        {
            PersonneDataAccess personneDataAccess = new PersonneDataAccess();
            return personneDataAccess.GetByEmail(email);
        }
        public void Insert(Personne entity)
        {
            ValidationHelper.ValidateStringField(entity.PersonneNom, "Personne_Nom", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonnePrenom, "Personne_Prenom", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonneVille, "Personne_Ville", 50, false);
            NumericValidationHelper.ValidatePositiveInt(entity.PersonneCodePostale, "Personne_Code_postale");
            ValidationHelper.ValidateStringField(entity.PersonneNomDeLaRue, "Personne_Nom_de_la_rue", 50, false);
            NumericValidationHelper.ValidatePositiveInt(entity.PersonneNumeroDeLaRue, "Personne_Numero_de_la_rue");
            ValidationHelper.ValidateStringField(entity.PersonneTelephone, "Personne_Telephone", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonneStationDeMetroLaPlusProche, "Personne_Station_de_metro_la_plus_proche", 50, false);
            _dataAccess.Insert(entity);
        }
        public void Update(Personne entity)
        {
            ValidationHelper.ValidateStringField(entity.PersonneNom, "Personne_Nom", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonnePrenom, "Personne_Prenom", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonneVille, "Personne_Ville", 50, false);
            NumericValidationHelper.ValidatePositiveInt(entity.PersonneCodePostale, "Personne_Code_postale");
            ValidationHelper.ValidateStringField(entity.PersonneNomDeLaRue, "Personne_Nom_de_la_rue", 50, false);
            NumericValidationHelper.ValidatePositiveInt(entity.PersonneNumeroDeLaRue, "Personne_Numero_de_la_rue");
            ValidationHelper.ValidateStringField(entity.PersonneTelephone, "Personne_Telephone", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonneStationDeMetroLaPlusProche, "Personne_Station_de_metro_la_plus_proche", 50, false);
            _dataAccess.Update(entity);
        }
        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
        public void DeleteByEmail(string email)
        {
            PersonneDataAccess personneDataAccess = new PersonneDataAccess();
            personneDataAccess.DeleteByEmail(email);
        }
    }
}
