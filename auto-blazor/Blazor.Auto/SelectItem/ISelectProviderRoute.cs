using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Auto.SelectItem
{
    public interface ISelectProviderRoute
    {
        Task<List<KeyValuePair<string, string>>> GetCacheAsync(string keyword);

        ConcurrentDictionary<string, List<KeyValuePair<string, string>>> ItemCache { get; set; }
    }
}
