using System;
using System.Xml.Schema;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/reporting")]
    public class ReportingController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public ReportingController(ILogger<ReportingController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpGet("{id}", Name = "getReportingById")]
        public IActionResult GetReportingById(String id)
        {
            var reporting = new ReportingStructure();

            _logger.LogDebug($"Received reporting structure get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            reporting.Employee = employee;
            reporting.NumberOfReports = NumberOfReports(employee);

            return Ok(reporting);
        }

        // Recursively traverse the tree of the reporting structure to count the nodes.
        public int NumberOfReports(Employee employee)
        {
            if (employee.DirectReports == null)
                return 0;

            var total = employee.DirectReports.Count;

            foreach (Employee report in employee.DirectReports)
            {
                total += NumberOfReports(report);
            }

            return total;
        }
    }
}
