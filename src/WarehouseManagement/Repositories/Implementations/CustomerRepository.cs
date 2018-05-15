using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private string _sqlConnectionString;

        public CustomerRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }
    }
}
