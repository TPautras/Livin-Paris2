using SqlConnector.DataService;

namespace SqlConnector.Models
{
    public interface ILpModels<T>
    {
        IDataAccess<T> DataAccess { get; }
        IDataService<T> DataService { get; }
    }
}