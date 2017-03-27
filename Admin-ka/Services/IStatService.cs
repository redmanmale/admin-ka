using System.Threading.Tasks;
using Admin.Model;

namespace Admin.Services
{
    public interface IStatService
    {
        Task<StatContract[]> GetStatsAsync(string type);

        Task<ModuleInfo> GetInfoAsync(string type);
    }
}
