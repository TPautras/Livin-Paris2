using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;

namespace SqlConnector.DataService
{
    public class CompositionDeLaRecetteService : IDataService<CompositionDeLaRecette>
    {
        private readonly IDataAccess<CompositionDeLaRecette> _dataAccess;

        public CompositionDeLaRecetteService(IDataAccess<CompositionDeLaRecette> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<CompositionDeLaRecette> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public CompositionDeLaRecette GetById(int id)
        {
            throw new NotImplementedException("Cette entité possède une clé composite.");
        }

        public void Insert(CompositionDeLaRecette entity)
        {
            _dataAccess.Insert(entity);
        }

        public void Update(CompositionDeLaRecette entity)
        {
            throw new NotImplementedException("Mise à jour non supportée pour une clé composite.");
        }

        public void Delete(int id)
        {
            throw new NotImplementedException("Suppression non supportée pour une clé composite.");
        }
    }
}