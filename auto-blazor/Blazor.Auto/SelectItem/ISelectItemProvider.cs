using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Auto.SelectItem
{
    public interface ISelectItemProvider
    {
        Task<List<KeyValuePair<string, string>>> GetSelectItemAsync();
    }
}
