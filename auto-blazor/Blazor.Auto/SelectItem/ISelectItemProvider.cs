using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazor.Auto.SelectItem
{
    public interface ISelectItemProvider
    {
        List<KeyValuePair<string, string>> Items { get; set; }
        Task<List<KeyValuePair<string, string>>> GetSelectItemAsync();
    }
}
