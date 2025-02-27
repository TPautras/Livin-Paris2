using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;

namespace SqlConnector.DataService
{
    public class FaitPartieDeService : IDataService<FaitPartieDe>
    {
        private readonly IDataAccess<FaitPartieDe> _dataAccess;

        public FaitPartieDeService(IDataAccess<FaitPartieDe> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<FaitPartieDe> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public FaitPartieDe GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite.");
        }

        public void Insert(FaitPartieDe entity)
        {
            _dataAccess.Insert(entity);
        }

        public void Update(FaitPartieDe entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite.");
        }
    }
}