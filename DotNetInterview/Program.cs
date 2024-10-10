using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConsole();
            });

            // register our services in the dependency container
			serviceCollection.AddSingleton<IApiService, ApiService>();
            serviceCollection.AddSingleton<IRegistryService, RegistryService>();
            serviceCollection.AddSingleton<ISoftwareReporter, SoftwareReporter>();

            var container = serviceCollection.BuildServiceProvider();

            // search for the software key in the registry and report whether it was found
            var softwareReporter = container.GetRequiredService<ISoftwareReporter>();
            await softwareReporter.ReportSoftwareInstallationStatus("Syncro");

            Console.WriteLine("\nPress any key to exit.");
            Console.ReadKey();
        }
    }
}
