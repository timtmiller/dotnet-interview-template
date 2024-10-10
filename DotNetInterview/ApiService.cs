using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface IApiService
    {
        // report the result of the registry search through the API
        Task<int> SendInstalledSoftware(string softwareName, bool isInstalled);
    }

    public class ApiService : IApiService
    {
        private readonly ILogger<ApiService> _logger;

        public ApiService(ILogger<ApiService> logger)
        {
	        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // report the result of the registry search through the API
        public async Task<int> SendInstalledSoftware(string softwareName, bool isInstalled)
        {
            _logger.LogInformation("Sending installation status for {softwareName}.  Installed: {isInstalled}",
                softwareName,
                isInstalled);

            // Simulate an HTTP request.  Normally you'd be awaiting an async
            // method on the HttpClient.
            var statusCode = await Task.Run(() =>
            {
                return 200;
            });

            return statusCode;
        }
    }
}
