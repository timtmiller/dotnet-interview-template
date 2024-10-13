using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface ISoftwareReporter
    {
        // determines whether software with the given name has been installed
        // on the current system and reports the result through the api service
        Task ReportSoftwareInstallationStatus(string softwareName);
    }

    public class SoftwareReporter : ISoftwareReporter
    {
        private readonly IRegistryService _registryService;
        private readonly IApiService _apiService;
        private readonly ILogger<SoftwareReporter> _logger;

        public SoftwareReporter(IRegistryService registryService, IApiService apiService, ILogger<SoftwareReporter> logger)
        {
            _registryService = registryService ?? throw new ArgumentNullException(nameof(registryService));
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // determines whether software with the given name has been installed
        // on the current system and reports the result through the api service
        public async Task ReportSoftwareInstallationStatus(string softwareName)
        {
            if (string.IsNullOrEmpty(softwareName) || (softwareName.Trim() == String.Empty)) throw new ArgumentNullException(nameof(softwareName));

            // check whether a key for the software is found in the registry and report result through the api
            bool isInstalled;
            string error;
            try
            {
                isInstalled = _registryService.CheckIfInstalled(softwareName);
                error = null;
                _logger.LogInformation($"Result of searching registry for software {softwareName}: {isInstalled}");

            }
            catch (Exception ex)
            {
                isInstalled = false;
                error = $"An error occurred searching for the software key: {ex.Message}";
            }

            await _apiService.SendInstalledSoftware(softwareName, isInstalled, error);
        }
    }
}
