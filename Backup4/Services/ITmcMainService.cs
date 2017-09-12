using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Services
{
    public interface ITmcMainService
    {
        #region Project
        void AddProject(Project entity);
        void UpdateProject(Project entity);
        void DeleteProject(Project entity);
        Project GetProject(int Id);
        IEnumerable<Project> GetAllProjects();
        IEnumerable<Project> GetPublishedProjects();
        #endregion

        #region ProjectType
        void AddProjectType(ProjectType entity);
        void UpdateProjectType(ProjectType entity);
        void DeleteProjectType(ProjectType entity);
        ProjectType GetProjectType(int Id);
        IEnumerable<ProjectType> GetAllProjectTypes(); 
        #endregion
    }
}
