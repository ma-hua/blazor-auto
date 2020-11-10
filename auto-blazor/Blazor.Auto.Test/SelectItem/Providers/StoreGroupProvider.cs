using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Blazor.Auto.SelectItem;

namespace Blazor.Auto.Test.SelectItem.Providers
{
    public class StoreGroupProvider : ISelectItemProvider
    {

        public Task<List<KeyValuePair<string, string>>> GetSelectItemAsync()
        {
            return Task.FromResult(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("1","门店组一"),
                new KeyValuePair<string, string>("2","门店组二")
            });
        }
    }
}
