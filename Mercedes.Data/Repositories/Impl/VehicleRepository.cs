using Dapper;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Impl
{
    public class VehicleRepository : GenericRepository<Vehicle>, IVehicleRepository
    {
        public Task<IEnumerable<Vehicle>> GetAllVehicles(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
