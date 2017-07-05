using Mercedes.Core.Domain;
using System.Collections.Generic;

namespace Mercedes.Data.Repositories.Contract
{
    public interface IPriceModelRepository : IGenericRepository<PriceModel>
    {
        IEnumerable<PriceModel> GetPriceModelByModel(int modelid);
    }
}
