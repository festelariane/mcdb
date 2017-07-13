using FastMapper;
using Mercedes.Admin.Models;
using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Admin.Extensions
{
    public static class MappingExtensions
    {
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
            return TypeAdapter.Adapt(model,destination);
        }

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
    }
}