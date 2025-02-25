using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;

namespace SqlConnector.DataService
{
    public class LivreService : IDataService<Livre>
    {
        private readonly IDataAccess<Livre> _dataAccess;

        public LivreService(IDataAccess<Livre> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Livre> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Livre GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite.");
        }

        public void Insert(Livre entity)
        {
            _dataAccess.Insert(entity);
        }

        public void Update(Livre entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite.");
        }
    }
}