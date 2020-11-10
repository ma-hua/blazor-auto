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

        public async Task<List<KeyValuePair<string, string>>> GetSelectItemAsync()
        {
            Console.WriteLine("StoreGroupProvider调用了一次");
            await Task.Delay(1000);
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("1","门店组一"),
                new KeyValuePair<string, string>("2","门店组二")
            };
        }
    }
}
