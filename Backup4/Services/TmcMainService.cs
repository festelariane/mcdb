using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using Mercedes.Plugins.TrinhMinh.WebApi.Data;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Services
{
    public class TmcMainService : ITmcMainService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        public TmcMainService(IProjectRepository projectRepository, IProjectTypeRepository projectTypeRepository)
        {
            _projectRepository = projectRepository;
            _projectTypeRepository = projectTypeRepository;
        }
        public void AddProject(Project entity)
        {
            _projectRepository.Add(entity);
        }

        public void AddProjectType(ProjectType entity)
        {
            _projectTypeRepository.Add(entity);
        }

        public void DeleteProject(Project entity)
        {
            _projectRepository.Delete(entity);
        }

        public void DeleteProjectType(ProjectType entity)
        {
            _projectTypeRepository.Delete(entity);
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return _projectRepository.GetAll();
        }

        public IEnumerable<ProjectType> GetAllProjectTypes()
        {
            return _projectTypeRepository.GetAll();
        }

        public Project GetProject(int Id)
        {
            return _projectRepository.Get(Id);
        }

        public ProjectType GetProjectType(int Id)
        {
            return _projectTypeRepository.Get(Id);
        }

        public IEnumerable<Project> GetPublishedProjects()
        {
            return _projectRepository.GetPublishedProjects();
        }

        public void UpdateProject(Project entity)
        {
            _projectRepository.Update(entity);
        }

        public void UpdateProjectType(ProjectType entity)
        {
            try
            {
                _projectTypeRepository.Update(entity);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
