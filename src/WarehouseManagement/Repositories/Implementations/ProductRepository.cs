using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private string _sqlConnectionString;

        public ProductRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }
    }
}
