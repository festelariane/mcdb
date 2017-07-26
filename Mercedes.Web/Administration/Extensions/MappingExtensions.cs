using FastMapper;
using Mercedes.Admin.Models;
using Mercedes.Admin.Models.Settings;
using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Extensions
{
    public static class MappingExtensions
    {
        #region Category
        public static CategoryModel ToModel(this Category entity)
        {
            return TypeAdapter.Adapt<Category, CategoryModel>(entity);
        }
        public static Category ToEntity(this CategoryModel model)
        {
            return TypeAdapter.Adapt<CategoryModel, Category>(model);
        }

        public static Category ToEntity(this CategoryModel model, Category destination)
        {
            return TypeAdapter.Adapt(model, destination);
        }
        #endregion

        #region VendorPictureMapping
        public static VehiclePictureModel ToModel(this Model_Image_Mapping entity)
        {
            return TypeAdapter.Adapt<Model_Image_Mapping, VehiclePictureModel>(entity);
        }
        public static Model_Image_Mapping ToEntity(this VehiclePictureModel model)
        {
            return TypeAdapter.Adapt<VehiclePictureModel, Model_Image_Mapping>(model);
        }

        public static Model_Image_Mapping ToEntity(this VehiclePictureModel model, Model_Image_Mapping destination)
        {
            return TypeAdapter.Adapt(model, destination);
        }
        #endregion

        #region Setting
        public static SettingModel ToModel(this Setting entity)
        {
            return TypeAdapter.Adapt<Setting, SettingModel>(entity);
        }
        public static Setting ToEntity(this SettingModel model)
        {
            return TypeAdapter.Adapt<SettingModel, Setting>(model);
        }

        public static Setting ToEntity(this SettingModel model, Setting destination)
        {
            return TypeAdapter.Adapt(model, destination);
        }
        #endregion
    }
}