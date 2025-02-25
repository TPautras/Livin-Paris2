using System.Collections.Generic;
using SqlConnector.Models;
using SqlConnector.DataAccess;
using SqlConnector.DataServices;

namespace SqlConnector.DataService
{
    public class EvaluationService : IDataService<Evaluation>
    {
        private readonly IDataAccess<Evaluation> _dataAccess;

        public EvaluationService(IDataAccess<Evaluation> dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public List<Evaluation> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Evaluation GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Evaluation entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.EvaluationId, "Evaluation_Id");
            NumericValidationHelper.ValidatePositiveDecimal(entity.EvaluationClient, "Evaluation_Client");
            NumericValidationHelper.ValidatePositiveDecimal(entity.EvaluationCuisinier, "Evaluation_Cuisinier");
            // Pour les descriptions, on peut autoriser des chaînes vides
            ValidationHelper.ValidateStringField(entity.EvaluationDescriptionClient, "Evaluation_Description_Client", 500);
            ValidationHelper.ValidateStringField(entity.EvaluationDescriptionCuisinier, "Evaluation_Description_Cuisinier", 500);
            NumericValidationHelper.ValidatePositiveInt(entity.CommandeId, "Commande_Id");

            _dataAccess.Insert(entity);
        }

        public void Update(Evaluation entity)
        {
            NumericValidationHelper.ValidatePositiveInt(entity.EvaluationId, "Evaluation_Id");
            NumericValidationHelper.ValidatePositiveDecimal(entity.EvaluationClient, "Evaluation_Client");
            NumericValidationHelper.ValidatePositiveDecimal(entity.EvaluationCuisinier, "Evaluation_Cuisinier");
            ValidationHelper.ValidateStringField(entity.EvaluationDescriptionClient, "Evaluation_Description_Client", 500);
            ValidationHelper.ValidateStringField(entity.EvaluationDescriptionCuisinier, "Evaluation_Description_Cuisinier", 500);
            NumericValidationHelper.ValidatePositiveInt(entity.CommandeId, "Commande_Id");

            _dataAccess.Update(entity);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}
