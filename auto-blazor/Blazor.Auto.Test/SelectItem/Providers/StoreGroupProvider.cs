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
        public const string Keyword = "StoreGroup";

        public List<KeyValuePair<string, string>> Items { get; set; }

        public Task<List<KeyValuePair<string, string>>> GetSelectItemAsync()
        {
            if (Items == null || !Items.Any())
            {
                Thread.Sleep(2000);
                Items = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("1","门店组一"),
                    new KeyValuePair<string, string>("2","门店组二")
                };
                Console.WriteLine("Items 进行了一次赋值");
            }

            return Task.FromResult(Items);
        }
    }
}
