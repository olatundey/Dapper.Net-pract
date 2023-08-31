using System.Threading.Tasks;

namespace DapperPractData.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters,
            string connectionId = "DefaultConnection");
        Task SaveData<P>
            (string query, P parameters, string connectionId = "DefaultConnection");
    }
}