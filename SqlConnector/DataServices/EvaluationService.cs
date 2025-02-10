using System.Collections.Generic;
using SqlConnector.Models;

namespace SqlConnector.DataServices
{
    public class EvaluationService
    {
        private readonly EvaluationDataAccess _dataAccess = new EvaluationDataAccess();

        public List<Evaluation> GetAll()
        {
            return _dataAccess.GetAll();
        }

        public Evaluation GetById(int id)
        {
            return _dataAccess.GetById(id);
        }

        public void Insert(Evaluation eval)
        {
            _dataAccess.Insert(eval);
        }

        public void Update(Evaluation eval)
        {
            _dataAccess.Update(eval);
        }

        public void Delete(int id)
        {
            _dataAccess.Delete(id);
        }
    }
}