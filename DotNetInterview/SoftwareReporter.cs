using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInterview
{
    public interface ISoftwareReporter
    {
        Task ReportSoftwareInstallationStatus(string softwareName);
    }

    public class SoftwareReporter : ISoftwareReporter
    {
        // TODO: Finish implementing this class so the unit tests
        // in SoftwareReporterTests are passing.

        public Task ReportSoftwareInstallationStatus(string softwareName)
        {
            throw new NotImplementedException();
        }
    }
}
