using System;
using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class CuisinierService : IDataService<Cuisinier>
    {
        private readonly IDataAccess<Cuisinier> _dataAccess;
        public CuisinierService(IDataAccess<Cuisinier> dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public List<Cuisinier> GetAll()
        {
            return _dataAccess.GetAll();
        }
        public Cuisinier GetById(int id)
        {
            return _dataAccess.GetById(id);
        }
        public void Insert(Cuisinier entity)
        {
            ValidationHelper.ValidateStringField(entity.CuisinierUsername, "Cuisinier_Username", 50, false);
            ValidationHelper.ValidateStringField(entity.CuisinierPassword, "Cuisinier_Password", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonneEmail, "Personne_Email", 50, false);
            _dataAccess.Insert(entity);
        }
        public void Update(Cuisinier entity)
        {
            ValidationHelper.ValidateStringField(entity.CuisinierUsername, "Cuisinier_Username", 50, false);
            ValidationHelper.ValidateStringField(entity.CuisinierPassword, "Cuisinier_Password", 50, false);
            ValidationHelper.ValidateStringField(entity.PersonneEmail, "Personne_Email", 50, false);
            _dataAccess.Update(entity);
        }
        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }

        public Cuisinier GetByUsername(string username)
        {
            CuisinierDataAccess cuisinierDataAccess = new CuisinierDataAccess();
            return cuisinierDataAccess.GetByUsername(username);
        }
        public bool TestConnection(string username, string password)
        {
            CuisinierDataAccess dataAccess = new CuisinierDataAccess();
            CuisinierService clientService = new CuisinierService(dataAccess);
            Cuisinier cuisinier;
            try
            {
                 cuisinier = clientService.GetByUsername(username);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
                return false;
            }

            if (cuisinier != null)
            {
                return true;
            }
            return false;
        }
    }
}