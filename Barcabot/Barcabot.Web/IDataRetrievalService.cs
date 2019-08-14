using System.Threading.Tasks;

namespace Barcabot.Web
{
    public interface IDataRetrievalService
    {
        Task<T> RetrieveData<T>(string url);
    }
}