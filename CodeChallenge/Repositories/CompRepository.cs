using System;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Data;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.Extensions.Logging;

namespace CodeChallenge.Repositories
{
    public class CompRepository : ICompRepository
    {
        private readonly CompContext _compContext;
        private readonly ILogger<ICompRepository> _logger;

        public CompRepository(ILogger<ICompRepository> logger, CompContext compContext)
        {
            _compContext = compContext;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            _compContext.Compensations.Add(compensation);
            return compensation;
        }
        
        public Compensation GetById(String id)
        {
            return _compContext.Compensations.SingleOrDefault(c => c.Employee.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _compContext.SaveChangesAsync();
        }
    }
}
