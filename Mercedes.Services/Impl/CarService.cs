﻿using Mercedes.Data.Repositories.Contract;
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
        private readonly IModelRepository _modelRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPriceModelRepository _priceModelRepository;
        public CarService(IVehicleRepository carRepository, IManufacturerRepository manufactureRepository, IModelRepository modelRepository
            , ICategoryRepository categoryRepository, IPriceModelRepository priceModelRepository)
        {
            _carRepository = carRepository;
            _manufactureRepository = manufactureRepository;
            _modelRepository = modelRepository;
            _categoryRepository = categoryRepository;
            _priceModelRepository = priceModelRepository;
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

        public Model GetModelById(int modelId)
        {
            return _modelRepository.Get(modelId);
        }

        public IList<Model> GetAllModel(bool includeDeletedItems = false)
        {
            if(includeDeletedItems)
            {
                return _modelRepository.GetAll().ToList();
            }
            return _modelRepository.GetAllExceptDeletedItems().ToList();
        }

        public IList<Model> GetModelByCategoryId(int categoryId)
        {
            return _modelRepository.GetByCategoryId(categoryId).ToList();
        }

        public Model GetModelDetail(int id)
        {
            return _modelRepository.Get(id);
        }
        #region Car Model

        public bool AddModel(Model model)
        {
            try
            {
                _modelRepository.Add(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteModel(Model model)
        {
            try
            {
                _modelRepository.Delete(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateModel(Model model)
        {
            try
            {
                _modelRepository.Update(model);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool AddOrUpdateCarModel(Model model)
        {
            try
            {
                if (model.Id > 0)
                {
                    _modelRepository.Update(model);
                }
                else
                {
                    _modelRepository.Add(model);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        } 
        #endregion

        // Category
        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.Get(categoryId);
        }

        public IList<Category> GetAllCategory()
        {
            return _categoryRepository.GetAll().ToList();
        }

        public bool AddCategory(Category category)
        {
            try
            {
                _categoryRepository.Add(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCategory(Category category)
        {
            try
            {
                _categoryRepository.Delete(category);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool SaveCategory(Category category)
        {
            try
            {
                if(category.Id > 0)
                {
                    _categoryRepository.Update(category);
                }
                else
                {
                    _categoryRepository.Add(category);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       
        public IList<PriceModel> GetPriceModelByModel(int modelid)
        {
            return _priceModelRepository.GetPriceModelByModel(modelid).ToList();
        }

        public IList<Model_Image_Mapping> GetVehicleModelImageUrl(int vehicleModelId)
        {
            return _modelRepository.GetVehicleModelImageUrl(vehicleModelId).ToList();
        }
        public Manufacturer GetManufacturerByModelId(int modelId)
        {
            return _manufactureRepository.GetManufacturerByModelId(modelId);
        }

        public IList<Model_Image_Mapping> GetModelPictures(int modelId)
        {
            return _modelRepository.GetVehicleModelImageUrl(modelId).ToList();
        }

        public bool AddModelImage(Model_Image_Mapping picture)
        {
            _modelRepository.AddModelImage(picture);
            return true;
        }
        public bool DeleteModelImage(int Id)
        {
            try
            {
                _modelRepository.DeleteModelImage(new Model_Image_Mapping() {Id = Id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
