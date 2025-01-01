using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Newtonsoft.Json;

namespace CodeChallenge.Data
{
    public class CompDataSeeder
    {
        private CompContext _compContext;

        public CompDataSeeder(CompContext compContext )
        {
            _compContext = compContext;
        }
    }
}
