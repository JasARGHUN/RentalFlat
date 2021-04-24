using Blazored.LocalStorage;
using ClientApp.Services;
using ClientApp.Services.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ClientApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(
                builder.Configuration.GetValue<string>("BaseAPIUrl")) // Getting API Url to clientapp. Don't forget to change settings in wwwroot/appsettings.json!!!
            });

            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

            builder.Services.AddScoped<IFlatService, FlatService>();
            builder.Services.AddScoped<IHomepageService, HomepageService>();
            builder.Services.AddScoped<IOrderDetailsService, OrderDetailsService>();
            builder.Services.AddScoped<IStripePaymentService, StripePaymentService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IInfoBlockService, InfoBlockService>();

            await builder.Build().RunAsync();
        }
    }
}
