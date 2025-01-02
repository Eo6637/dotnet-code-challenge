using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CodeChallenge.Models;
using CodeChallenge.Tests.Integration.Extensions;
using CodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingById_Returns_Ok()
        {
            // Verify that 'Lord Business' has 7 employees listed in his reporting structure
            var employeeId = "e368906b-ae77-487f-b8cf-953236e3e5f0";
            var expectedFirstName = "Lord";
            var expectedLastName = "Business";
            var expectedNumReports = 7;

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reporting/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reporting = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(expectedFirstName, reporting.Employee.FirstName);
            Assert.AreEqual(expectedLastName, reporting.Employee.LastName);
            Assert.AreEqual(expectedNumReports, reporting.NumberOfReports);
        }
    }
}
