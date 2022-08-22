using System.Threading.Tasks;

namespace ConsumeGraphAPI.Services
{
    public interface IMainServices
    {
        Task<string> GetDataByAuthToken();
    }
}
