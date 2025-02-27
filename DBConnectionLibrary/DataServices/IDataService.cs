using System.Collections.Generic;

namespace SqlConnector.DataService
{
    public interface IDataService<T>
    {
        List<T> GetAll();
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}