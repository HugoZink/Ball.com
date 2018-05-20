using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Repositories
{
    public interface ITransportRepository
    {
        Task<IEnumerable<Transport>> GetTransportsAsync();
        Task<Transport> GetTransportAsync(string transportId);
    }
}
