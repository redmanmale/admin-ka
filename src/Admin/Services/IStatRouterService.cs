using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Controllers;
using Admin.Model;

namespace Admin.Services
{
    public interface IStatRouterService
    {
        Task<IReadOnlyDictionary<string, StatContract>> GetStatsAsync(string type);
    }
}