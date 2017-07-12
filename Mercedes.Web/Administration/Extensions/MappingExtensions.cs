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
    }
}