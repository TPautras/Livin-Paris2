using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class PlatService
    {
        private readonly PlatDataAccess _dataAccess = new PlatDataAccess();

        public List<Plat> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Plat GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Plat plat)
        {
            ValidatePlat(plat);
            _dataAccess.Insert(plat);
        }

        public void Update(Plat plat)
        {
            ValidatePlat(plat);
            _dataAccess.Update(plat);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }

        private void ValidatePlat(Plat p)
        {
            ValidationHelper.ValidateStringField(p.PlatNom, "Plat_Nom", 50);
            ValidationHelper.ValidateStringField(p.PlatOrigine, "Plat_Origine", 50);
            ValidationHelper.ValidateStringField(p.PlatAromesNaturels, "Plat_Aromes_naturels", 100);
            ValidationHelper.ValidateStringField(p.PlatTypeDePlat, "Plat_Type_de_plat", 50);
            ValidationHelper.ValidateStringField(p.PlatRegimeAlimentaire, "Plat_Regime_alimentaire", 50);
        }
    }
}