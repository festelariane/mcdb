using FastMapper;
using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using Mercedes.Plugins.TrinhMinh.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Extensions
{
    public static class MappingExtensions
    {
        #region Project Management
        public static ProjectModel ToModel(this Project entity)
        {
            return TypeAdapter.Adapt<Project, ProjectModel>(entity);
        }
        public static Project ToEntity(this ProjectModel model)
        {
            return TypeAdapter.Adapt<ProjectModel, Project>(model);
        }

        public static Project ToEntity(this ProjectModel model, Project destination)
        {
            return TypeAdapter.Adapt(model, destination);
        }
        #endregion

        #region ProjectType Management
        public static ProjectTypeModel ToModel(this ProjectType entity)
        {
            return TypeAdapter.Adapt<ProjectType, ProjectTypeModel>(entity);
        }
        public static ProjectType ToEntity(this ProjectTypeModel model)
        {
            return TypeAdapter.Adapt<ProjectTypeModel, ProjectType>(model);
        }

        public static ProjectType ToEntity(this ProjectTypeModel model, ProjectType destination)
        {
            return TypeAdapter.Adapt(model, destination);
        }
        #endregion
    }
}
