using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;

namespace Mercedes.Services.Impl
{
    public class RentService : IRentService
    {
        private readonly IRentTypeRepository _rentTypeRepository;
        private readonly IPriceModelRepository _priceModelRepository;
        public RentService(IRentTypeRepository rentTypeRepository, IPriceModelRepository priceModelRepository)
        {
            _rentTypeRepository = rentTypeRepository;
            _priceModelRepository = priceModelRepository;
        }

        public bool AddPriceModel(PriceModel priceModel)
        {
            try
            {
                _priceModelRepository.Add(priceModel);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }            
        }

        public bool AddRentType(RentType rentType)
        {
            try
            {
                _rentTypeRepository.Add(rentType);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeletePriceModel(PriceModel priceModel)
        {
            try
            {
                _priceModelRepository.Delete(priceModel);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteRentType(RentType rentType)
        {
            try
            {
                _rentTypeRepository.Delete(rentType);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IList<PriceModel> GetAllPriceModels()
        {
            return _priceModelRepository.GetAll().ToList();
        }

        public IList<PriceModel> GetAllPriceModelsByModelId(int modelId)
        {
            return _priceModelRepository.GetAllPriceModelsByModelId(modelId).ToList();
        }

        public IList<RentType> GetAllRentTypes()
        {
            return _rentTypeRepository.GetAll().ToList();
        }

        public PriceModel GetPriceModelById(int priceModelId)
        {
            return _priceModelRepository.Get(priceModelId);
        }

        public RentType GetRentTypeById(int rentTypeId)
        {
            return _rentTypeRepository.Get(rentTypeId);
        }

        public bool UpdatePriceModel(PriceModel priceModel)
        {
            try
            {
                _priceModelRepository.Update(priceModel);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateRentType(RentType rentType)
        {
            try
            {
                _rentTypeRepository.Update(rentType);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
