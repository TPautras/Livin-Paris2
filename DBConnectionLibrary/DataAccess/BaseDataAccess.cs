using System.Data.SqlClient;

namespace LivinParis.DataAccess
{
    public abstract class BaseDataAccess
    {
        protected readonly string ConnectionString = "Server=localhost;Port=3306;Database=livin_paris;Uid=root;Password=root;";

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}