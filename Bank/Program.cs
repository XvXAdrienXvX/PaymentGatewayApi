using Bank.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();
        }

        static async Task MainAsync()
        {
            var services = ConfigureServices();

            var serviceProvider = services.BuildServiceProvider();

            try
            {
                await serviceProvider.GetService<App>().Run();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IBankService, BankService>();
            services.AddTransient<App>();

            return services;
        }
    }
}
