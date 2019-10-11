using System.Collections.Generic;
using System.Threading.Tasks;
using Admin.Model;

namespace Admin.Services
{
    public interface IStatService
    {
        Task<IReadOnlyDictionary<string, Stat>> GetStats();
    }
}
