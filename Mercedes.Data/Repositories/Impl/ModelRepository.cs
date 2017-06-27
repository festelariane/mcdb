using Mercedes.Data.Repositories.Contract;
using System.Linq;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class ModelRepository : BaseRepository, IModelRepository
    {
        public Model Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Model lrs inner join Manufacturer l on l.Id=lrs.ManufacturerId where lrs.Id=@Id";
                var result = conn.Query<Model, Manufacturer, Model>(query, (item, manufacturer) =>
                {
                    item.Manufacturer = manufacturer;
                    return item;
                }, new { Id = Id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Model> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Model lrs inner join Manufacturer l on l.Id=lrs.ManufacturerId";
                var result = conn.Query<Model, Manufacturer, Model>(query, (item, manufacturer) =>
                {
                    item.Manufacturer = manufacturer;
                    return item;
                });
                return result;
            }
        }

        public void Add(Model entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into Model (ManufacturerId,Code,Name) values (@ManufacturerId,@Code,@Name)";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name,ManufacturerId=entity.ManufacturerId });
            }
        }

        public void Delete(Model entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete Model where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(Model entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update Model set ManufacturerId=@ManufacturerId, Code=@Code, Name=@Name where Id=@Id";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name, Id = entity.Id,ManufacturerId=entity.ManufacturerId });
            }
        }
    }
}
