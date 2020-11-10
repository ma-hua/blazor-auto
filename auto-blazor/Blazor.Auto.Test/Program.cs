using System;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Blazor.Auto.SelectItem;
using Blazor.Auto.Test.SelectItem;
using Blazor.Auto.Test.SelectItem.Providers;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Blazor.Auto.Test
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddAntDesign();

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddSingleton<ISelectProviderRoute, SelectProviderRoute>();
            builder.ConfigureContainer(new AutofacServiceProviderFactory(cfg =>
                {
                    cfg.RegisterAssemblyTypes(Assembly.GetExecutingAssembly());
                    cfg.RegisterType<StoreGroupProvider>().Named<ISelectItemProvider>(SelectProviderRoute.StoreGroup).InstancePerDependency();
                    cfg.RegisterType<PriceGroupProvider>().Named<ISelectItemProvider>(SelectProviderRoute.PriceGroup).InstancePerDependency();
                }));

            await builder.Build().RunAsync();
        }
    }
}
