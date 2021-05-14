using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace DotNetInterview.Tests
{
    [TestClass]
    public class SoftwareReporterTests
    {
        private Mock<IApiService> _apiMock;
        private Mock<IRegistryService> _registryMock;
        private Mock<ILogger<SoftwareReporter>> _loggerMock;
        private SoftwareReporter _softwareReporter;

        [TestInitialize]
        public void Init()
        {
            _apiMock = new Mock<IApiService>();
            _registryMock = new Mock<IRegistryService>();
            _loggerMock = new Mock<ILogger<SoftwareReporter>>();
            _softwareReporter = new SoftwareReporter(_registryMock.Object, _apiMock.Object, _loggerMock.Object);
        }

        [TestMethod]
        public async Task IsSoftwareInstalled_GivenNullOrEmptyValue_Throws()
        {
            try
            {
                await _softwareReporter.ReportSoftwareInstallationStatus(null);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }

            try
            {
                await _softwareReporter.ReportSoftwareInstallationStatus(string.Empty);
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }

            try
            {
                await _softwareReporter.ReportSoftwareInstallationStatus("     \r\n\t");
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex is ArgumentNullException);
            }
        }

        [TestMethod]
        public async Task IsSoftwareInstalled_GivenSoftwareNameNotFound_SendsFalse()
        {
            // Setup
            _registryMock.Setup(x => x.CheckIfInstalled(It.IsAny<string>())).Returns(false);

            // Act
            await _softwareReporter.ReportSoftwareInstallationStatus("Syncro");

            // Verify
            _apiMock.Verify(x => x.SendInstalledSoftware("Syncro", false));
        }

        [TestMethod]
        public async Task IsSoftwareInstalled_GivenSoftwareNameFound_SendsTrue()
        {
            // Setup
            _registryMock.Setup(x => x.CheckIfInstalled(It.IsAny<string>())).Returns(true);

            // Act
            await _softwareReporter.ReportSoftwareInstallationStatus("Syncro");

            // Verify
            _apiMock.Verify(x => x.SendInstalledSoftware("Syncro", true));
        }
    }
}
