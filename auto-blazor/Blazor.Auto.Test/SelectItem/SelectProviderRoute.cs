using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Blazor.Auto.SelectItem;

namespace Blazor.Auto.Test.SelectItem
{
    public class SelectProviderRoute : ISelectProviderRoute
    {
        public const string PriceGroup = "PriceGroup";
        public const string StoreGroup = "StoreGroup";

        private readonly SemaphoreSlim _mutex = new SemaphoreSlim(1);
        public ConcurrentDictionary<string, List<KeyValuePair<string, string>>> ItemCache { get; set; } = new ConcurrentDictionary<string, List<KeyValuePair<string, string>>>();
        
        private readonly IComponentContext _componentContext;
        public SelectProviderRoute(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public static string GetProviderName(object keyword)
        {
            switch (keyword)
            {
                case string str when str.Contains(PriceGroup): return PriceGroup;
                case string str when str.Contains(StoreGroup): return StoreGroup;
                default: return string.Empty;
            }
        }

        public async Task<List<KeyValuePair<string, string>>> GetCacheAsync(string keyword)
        {

            try
            {
                await _mutex.WaitAsync();

                if (ItemCache.Keys.Contains(keyword))
                {
                    return ItemCache.GetValueOrDefault(keyword, new List<KeyValuePair<string, string>>());
                }
                else
                {
                    var items = await GetProviderTask(keyword);
                    if (ItemCache.TryAdd(keyword, items))
                    {
                        return items;
                    }
                }
            }
            finally
            {
                _mutex.Release();
            }
            
            

            return new List<KeyValuePair<string, string>>();
        }

        private Task<List<KeyValuePair<string, string>>> GetProviderTask(string keyword) => 
            _componentContext.ResolveNamed<ISelectItemProvider>(keyword).GetSelectItemAsync();
    }
}
