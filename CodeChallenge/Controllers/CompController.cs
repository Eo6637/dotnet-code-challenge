using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Runtime.CompilerServices;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ICompService _compService;
        private readonly IEmployeeService _employeeService;

        public CompController(ILogger<CompController> logger, ICompService compService, IEmployeeService employeeService)
        {
            _logger = logger;
            _compService = compService;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateComp([FromBody] Compensation compensation)
        {
            _logger.LogDebug($"Received compensation create request for '{compensation.Employee.FirstName} {compensation.Employee.LastName}'");

            _compService.Create(compensation);

            return CreatedAtRoute("getCompById", new { id = compensation.Employee.EmployeeId }, compensation);
        }

        [HttpGet("{id}", Name = "getCompById")]
        public IActionResult GetCompById(String id)
        {
            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compService.GetById(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }
    }
}
