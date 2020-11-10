using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.Auto.SelectItem;

namespace Blazor.Auto.Test.SelectItem.Providers
{
    public class PriceGroupProvider : ISelectItemProvider
    {
        public Task<List<KeyValuePair<string, string>>> GetSelectItemAsync()
        {
            return Task.FromResult(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("1","价格组一"),
                new KeyValuePair<string, string>("2","价格组二")
            });
        }

    }
}
