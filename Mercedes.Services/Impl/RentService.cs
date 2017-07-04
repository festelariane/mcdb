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
        public RentService(IRentTypeRepository rentTypeRepository)
        {
            _rentTypeRepository = rentTypeRepository;
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

        public IList<RentType> GetAllRentTypes()
        {
            return _rentTypeRepository.GetAll().ToList();
        }

        public RentType GetRentTypeById(int rentTypeId)
        {
            return _rentTypeRepository.Get(rentTypeId);
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
