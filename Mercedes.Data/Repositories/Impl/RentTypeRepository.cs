using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class RentTypeRepository : BaseRepository, IRentTypeRepository
    {
        public RentType Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from RentType where Id=@Id";
                var result = conn.QueryFirst<RentType>(query, new { Id = Id});
                return result;
            }
        }

        public IEnumerable<RentType> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from RentType";
                var result = conn.Query<RentType>(query);
                return result;
            }
        }

        public void Add(RentType entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into RentType (RentTypeSystemName,RentTypeName,Published,DisplayOrder) values (@RentTypeSystemName,@RentTypeName,@Published,@DisplayOrder)";
                var result = conn.Query(query, new { RentTypeName = entity.RentTypeName, RentTypeSystemName = entity.RentTypeSystemName, Published  = entity.Published, DisplayOrder = entity.DisplayOrder });
            }
        }

        public void Delete(RentType entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete RentType where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(RentType entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update RentType set RentTypeSystemName=@RentTypeSystemName, RentTypeName=@RentTypeName,DisplayOrder=@DisplayOrder,Published=@Published where Id=@Id";
                var result = conn.Query(query, new { RentTypeSystemName = entity.RentTypeSystemName, RentTypeName = entity.RentTypeName, Published = entity.Published, DisplayOrder = entity.DisplayOrder, Id = entity.Id });
            }
        }
    }
}
