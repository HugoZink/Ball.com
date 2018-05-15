using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WarehouseManagement.Repositories.Implementations
{
    public class TransportRepository : ITransportRepository
    {
        private string _sqlConnectionString;

        public TransportRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }
    }
}
