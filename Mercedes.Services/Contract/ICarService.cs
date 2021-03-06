﻿using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface ICarService
    {
        Manufacturer GetManufacturerById(int manufacturerId);
        Manufacturer GetManufacturerByModelId(int modelId);
        IList<Manufacturer> GetAllManufacturers();
        bool AddManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(Manufacturer manufacturer);
        bool UpdateManufacturer(Manufacturer manufacturer);
        //Model class
        Model GetModelById(int modelId);
        IList<Model> GetAllModel(bool includeDeleted = false);
        bool AddModel(Model model);
        bool DeleteModel(Model model);
        bool UpdateModel(Model model);
        bool AddOrUpdateCarModel(Model category);

        IList<Model> GetModelByCategoryId(int categoryId);
        Model GetModelDetail(int id);        

        // Category
        Category GetCategoryById(int categoryId);
        IList<Category> GetAllCategory();
        bool DeleteCategory(Category category);
        bool SaveCategory(Category category);

        //PriceModel
        IList<PriceModel> GetPriceModelByModel(int modelid);

        IList<Model_Image_Mapping> GetVehicleModelImageUrl(int vehicleModelId);

        IList<Model_Image_Mapping> GetModelPictures(int modelId);
        bool AddModelImage(Model_Image_Mapping picture);
        bool DeleteModelImage(int Id);
    }
}
