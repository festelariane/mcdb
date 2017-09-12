using Mercedes.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using Dapper;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Data
{
    public class ProjectRepository : BaseRepository, IProjectRepository
    {
        public void Add(Project entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into [tmc_Project] (ProjectTypeId,Name,WebsiteUrl,ImageUrl,DisplayOrder,Published) values (@ProjectTypeId,@Name,@WebsiteUrl,@ImageUrl,@DisplayOrder,@Published)";
                var result = conn.Query(query, new
                {
                    ProjectTypeId = entity.ProjectTypeId,
                    Name = entity.Name,
                    WebsiteUrl = entity.WebsiteUrl,
                    ImageUrl = entity.ImageUrl,
                    Published = entity.Published,
                    DisplayOrder = entity.DisplayOrder
                });
            }
        }
        
        public void Delete(Project entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete tmc_Project where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public Project Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from tmc_Project where Id=@Id";
                var result = conn.Query<Project>(query, new { Id = Id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Project> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from tmc_Project";
                var result = conn.Query<Project>(query);
                return result;
            }
        }
        public IEnumerable<Project> GetPublishedProjects()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from tmc_Project where Published=1";
                var result = conn.Query<Project>(query);
                return result;
            }
        }
        public IEnumerable<Project> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update tmc_Project set ProjectTypeId=@ProjectTypeId,Name=@Name,WebsiteUrl=@WebsiteUrl,ImageUrl=@ImageUrl,DisplayOrder=@DisplayOrder,Published=@Published where Id=@Id";
                var result = conn.Query(query, new
                {
                    ProjectTypeId = entity.ProjectTypeId,
                    Name = entity.Name,
                    WebsiteUrl = entity.WebsiteUrl,
                    ImageUrl = entity.ImageUrl,
                    Published = entity.Published,
                    DisplayOrder = entity.DisplayOrder,
                    Id = entity.Id
                });
            }
        }
    }
}
