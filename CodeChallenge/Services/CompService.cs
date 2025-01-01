using System;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;
using CodeChallenge.Models;

namespace CodeChallenge.Services
{
    public class CompService : ICompService 
    {
        private readonly ICompRepository _compRepository;
        private readonly ILogger<CompService> _logger;

        public CompService(ILogger<CompService> logger, ICompRepository compRepository)
        {
            _compRepository = compRepository;
            _logger = logger;
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation != null)
            {
                _compRepository.Add(compensation);
                _compRepository.SaveAsync().Wait();
            }

            return compensation;
        }

        public Compensation GetById(String id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _compRepository.GetById(id);
            }

            return null;
        }
    }
}
