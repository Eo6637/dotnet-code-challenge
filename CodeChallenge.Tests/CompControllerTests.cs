using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using CodeChallenge.Models;

using CodeChallenge.Tests.Integration.Extensions;
using CodeChallenge.Tests.Integration.Helpers;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompControllerTests
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
        public void CreateComp_Returns_Created()
        {
            // Create compensation for existing employee 'Pete Best'
            var employee = new Employee()
            {
                EmployeeId = "03aa1462-ffa9-4978-1234-7c001562cf6f",
                Department = "Engineering",
                FirstName = "Pete",
                LastName = "Best",
                Position = "Developer VI"
            };
            var compensation = new Compensation()
            {
                Employee = employee,
                EmployeeId = employee.EmployeeId,
                Salary = 75000,
                EffectiveDate = new DateTime(2024, 1, 23)
            };

            var requestContent = new JsonSerialization().ToJson(compensation);

            // Execute
            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newComp = response.DeserializeContent<Compensation>();
            Assert.IsNotNull(newComp.Employee.EmployeeId);
            Assert.AreEqual(compensation.Employee.FirstName, newComp.Employee.FirstName);
            Assert.AreEqual(compensation.Employee.LastName, newComp.Employee.LastName);
            Assert.AreEqual(compensation.Employee.Department, newComp.Employee.Department);
            Assert.AreEqual(compensation.Employee.Position, newComp.Employee.Position);
            Assert.AreEqual(compensation.Salary, newComp.Salary);
            Assert.AreEqual(compensation.EffectiveDate, newComp.EffectiveDate);
        }

        [TestMethod]
        public void GetCompByIdReturnsOk()
        {
            // Arrange
            var employeeId = "03aa1462-ffa9-4978-1234-7c001562cf6f";
            var expectedFirstName = "Pete";
            var expectedLastName = "Best";
            var expectedSalary = 75000;
            var expectedEffective = new DateTime(2024, 1, 23);

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(expectedSalary, compensation.Salary);
            Assert.AreEqual(expectedEffective, compensation.EffectiveDate);
            Assert.AreEqual(expectedFirstName, compensation.Employee.FirstName);
            Assert.AreEqual(expectedLastName, compensation.Employee.LastName);
        }
    }
}
