using System.Collections.Generic;
using System.Threading.Tasks;
using Barcabot.Common.DataModels;
using Barcabot.Common.DataModels.Dto.ApiFootball;

namespace Barcabot.Web
{
    public interface IPlayerRetriever
    {
        Task<List<Player>> Retrieve();
    }
}