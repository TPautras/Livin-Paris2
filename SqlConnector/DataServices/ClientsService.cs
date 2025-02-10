using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class ClientsService
    {
        private readonly ClientsDataAccess _dataAccess = new ClientsDataAccess();
        private readonly PersonneService _personneService = new PersonneService();

        public List<Clients> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Clients GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Clients c)
        {
            // Comme Clients hérite de Personne, on doit d'abord s'assurer
            // que la Personne associée est insérée ou valide
            // On peut soit insérer la Personne puis le client, soit vérifier qu'elle existe déjà
            _personneService.Insert(c);
            _dataAccess.Insert(c);
        }

        public void Update(Clients c)
        {
            // Mise à jour de la Personne sous-jacente
            _personneService.Update(c);
            // S'il y a des champs spécifiques à la table Clients, on les gère ici
            _dataAccess.Update(c);
        }

        public void Delete(int id)
        {
            // Supprimer le client dans la table Clients
            _dataAccess.Delete(id);
            // Éventuellement, supprimer la Personne associée
            // (Cela dépend de votre logique métier, 
            //  vous pourriez vouloir la garder si c'est un "type" de Personne)
            // Ici, on pourrait faire :
            // _personneService.Delete(id);
        }
    }

}