using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface IApiService
    {
        // report the result of the registry search through the API
        Task<int> SendInstalledSoftware(string softwareName, bool isInstalled, string error);
    }

    public class ApiService : IApiService
    {
        private readonly ILogger<ApiService> _logger;

        public ApiService(ILogger<ApiService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // report the result of the registry search through the API
        public async Task<int> SendInstalledSoftware(string softwareName, bool isInstalled, string error)
        {
            int resultStatus;
            if (!string.IsNullOrEmpty(error))
            {
                _logger.LogError($"Sending failure result: {error}");
                resultStatus = 500;
            }
            else
            {
                _logger.LogInformation($"Sending installation status for {softwareName}.  Installed: {isInstalled}");
                resultStatus = 200;
            }

            // Simulate an HTTP request.  Normally you'd be awaiting an async
            // method on the HttpClient.
            var statusCode = await Task.Run(() =>
            {
                return resultStatus;
            });

            return statusCode;
        }
    }
}
