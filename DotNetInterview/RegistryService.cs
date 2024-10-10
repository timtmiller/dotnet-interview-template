using Microsoft.Extensions.Logging;
using Microsoft.Win32;
using System;

namespace DotNetInterview
{
    public interface IRegistryService
    {
        // searches the registry for a software key matching the specified name
        bool CheckIfInstalled(string softwareName);
    }

    public class RegistryService : IRegistryService
    {
        private readonly ILogger<RegistryService> _logger;

        public RegistryService(ILogger<RegistryService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // searches the registry for a software key matching the specified name
        public bool CheckIfInstalled(string softwareName)
        {
            if (String.IsNullOrEmpty(softwareName)) throw new ArgumentNullException(nameof(softwareName));
            softwareName = softwareName.Trim();
            if (softwareName == String.Empty) throw new ArgumentNullException(nameof(softwareName));

            // look for software key under current user key
            if (IsSoftwareKeyFound(Registry.CurrentUser, softwareName))
                return true;

            // if not found, look for software key under local machine key
            _logger.LogInformation("Searching local machine registry entries");
            return IsSoftwareKeyFound(Registry.LocalMachine, softwareName);
        }

        // returns whether a software key matching the specified name was found under the specified top-level key
        private bool IsSoftwareKeyFound(RegistryKey key, string softwareName)
        {
            using (RegistryKey subKey = key.OpenSubKey("SOFTWARE"))
            {
                if (subKey != null)
                {
                    string[] names = subKey.GetSubKeyNames();
                    foreach (string name in names)
                    {
                        // match the name against the subkey, ignoring case and allowing it to be a substring
                        if (name.IndexOf(softwareName, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            _logger.LogInformation($"Software key {softwareName} was found in registry under {key.Name}");
                            return true;
                        }
                    }
                }
            }
            _logger.LogInformation($"Software key {softwareName} was not found in registry under {key.Name}");
            return false;
        }
    }
}
