using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;

namespace SqlConnector.DataService
{
    public class CreationService : IDataService<Creation>
    {
        private readonly IDataAccess<Creation> _dataAccess;

        public CreationService(IDataAccess<Creation> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Creation> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Creation GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite. Utilisez une méthode dédiée.");
        }

        public void Insert(Creation entity)
        {
            // Vous pouvez ajouter ici des validations spécifiques aux clés composites si nécessaire.
            _dataAccess.Insert(entity);
        }

        public void Update(Creation entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite. Utilisez une méthode dédiée.");
        }
    }
}