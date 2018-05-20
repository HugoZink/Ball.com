using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories.Implementations
{
    public class EFTransportRepository : ITransportRepository
    {
        private string _sqlConnectionString;

        public EFTransportRepository(string sqlConnectionString)
        {
            _sqlConnectionString = sqlConnectionString;
        }

        public Task<IEnumerable<Transport>> GetTransportsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Transport> GetTransportAsync(string transportId)
        {
            throw new NotImplementedException();
        }
    }
}
