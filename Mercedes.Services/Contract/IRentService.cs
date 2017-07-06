using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface IRentService
    {
        RentType GetRentTypeById(int rentTypeId);
        IList<RentType> GetAllRentTypes();
        bool AddRentType (RentType rentType);
        bool DeleteRentType(RentType rentType);
        bool UpdateRentType(RentType rentType);

        //Price Model
        PriceModel GetPriceModelById(int priceModelId);
        IList<PriceModel> GetAllPriceModels();
        IList<PriceModel> GetAllPriceModelsByModelId(int modelId);
        bool AddPriceModel(PriceModel priceModel);
        bool DeletePriceModel(PriceModel priceModel);
        bool UpdatePriceModel(PriceModel priceModel);
    }
}
