using Mercedes.Data.Repositories;
using System;
using System.Collections.Generic;
using Dapper;
using Mercedes.Plugins.TrinhMinh.WebApi.Domain;
using System.Linq;

namespace Mercedes.Plugins.TrinhMinh.WebApi.Data
{
    public class ProjectTypeRepository : BaseRepository, IProjectTypeRepository
    {
        public void Add(ProjectType entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into [tmc_ProjectType] (SystemName,Name,DisplayOrder) values (@SystemName,@Name,@DisplayOrder)";
                var result = conn.Query(query, new
                {
                    SystemName = entity.SystemName,
                    Name = entity.Name,
                    DisplayOrder = entity.DisplayOrder
                });
            }
        }

        public void Delete(ProjectType entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete tmc_ProjectType where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public ProjectType Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from tmc_ProjectType where Id=@Id";
                var result = conn.Query<ProjectType>(query, new { Id = Id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<ProjectType> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from tmc_ProjectType";
                var result = conn.Query<ProjectType>(query);
                return result;
            }
        }

        public IEnumerable<ProjectType> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }

        public void Update(ProjectType entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update tmc_ProjectType set SystemName=@SystemName, Name=@Name, DisplayOrder=@DisplayOrder where Id=@Id";
                var result = conn.Query(query, new
                {
                    SystemName = entity.SystemName,
                    Name = entity.Name,
                    DisplayOrder = entity.DisplayOrder,
                    Id = entity.Id
                });
            }
        }
    }
}
