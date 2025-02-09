using System;
using System.Collections.Generic;

namespace SqlConnector.Services
{
    public class UtilisateurService
    {
        private readonly IDataAccess<User> _dataAccess;

        public UtilisateurService(IDataAccess<User> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<User> GetAllUtilisateurs() => _dataAccess.GetAll();
        public User GetUtilisateurById(int id) => _dataAccess.GetById(id);
        public void AjouterUtilisateur(User utilisateur) => _dataAccess.Insert(utilisateur);
        public void ModifierUtilisateur(User utilisateur) => _dataAccess.Update(utilisateur);
        public void SupprimerUtilisateur(int id) => _dataAccess.Delete(id);
    }
}
