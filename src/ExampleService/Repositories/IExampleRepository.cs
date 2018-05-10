using ExampleService.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExampleService.Repositories
{
    public interface IExampleRepository
    {
        Task<IEnumerable<Example>> GetExamplesAsync();
        Task<Example> GetExampleAsynch(string exampleId);
    }
}
