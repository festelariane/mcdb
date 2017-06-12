using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class ManufacturerRepository : BaseRepository, IManufacturerRepository
    {
        public Manufacturer Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Manufacturer where Id=@Id";
                var result = conn.QueryFirst<Manufacturer>(query, new { Id = Id});
                return result;
            }
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Manufacturer";
                var result = conn.Query<Manufacturer>(query);
                return result;
            }
        }

        public void Add(Manufacturer entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into Manufacturer (Code,Name) values (@Code,@Name)";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name });
            }
        }

        public void Delete(Manufacturer entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete Manufacturer where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(Manufacturer entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update Manufacturer set Code=@Code, Name=@Name where Id=@Id";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name, Id = entity.Id });
            }
        }
    }
}
