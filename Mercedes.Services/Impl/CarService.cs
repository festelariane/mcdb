using Mercedes.Data.Repositories.Contract;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;

namespace Mercedes.Services.Impl
{
    public class CarService : ICarService
    {
        private readonly IVehicleRepository _carRepository;
        private readonly IManufacturerRepository _manufactureRepository;
        public CarService(IVehicleRepository carRepository, IManufacturerRepository manufactureRepository)
        {
            _carRepository = carRepository;
            _manufactureRepository = manufactureRepository;
        }

        public bool AddManufacturer(Manufacturer manufacturer)
        {
            try
            {
                _manufactureRepository.Add(manufacturer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public bool DeleteManufacturer(Manufacturer manufacturer)
        {
            try
            {
                _manufactureRepository.Delete(manufacturer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool UpdateManufacturer(Manufacturer manufacturer)
        {
            try
            {
                _manufactureRepository.Update(manufacturer);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IList<Manufacturer> GetAllManufacturers()
        {
            return _manufactureRepository.GetAll().ToList();
        }

        public Manufacturer GetManufacturerById(int manufacturerId)
        {
            return _manufactureRepository.Get(manufacturerId);
        }
    }
}
