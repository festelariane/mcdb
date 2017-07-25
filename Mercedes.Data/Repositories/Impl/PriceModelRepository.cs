using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using Dapper;
using Mercedes.Core.Domain;
using System.Linq;

namespace Mercedes.Data.Repositories.Impl
{
   public class PriceModelRepository : BaseRepository, IPriceModelRepository
    {
        public void Add(PriceModel entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into PriceModel (RentTypeId,VehicleModelId,Price) values (@RentTypeId,@VehicleModelId,@Price)";
                var result = conn.Query(query, new { RentTypeId = entity.RentTypeId, VehicleModelId = entity.ModelId, Price = entity.Price });
            }
        }

        public void Delete(PriceModel entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete PriceModel where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public PriceModel Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = @"select * from PriceModel lrs inner join RentType l on l.Id=lrs.RentTypeId 
                    inner join Model m on lrs.VehicleModelId = m.Id where lrs.Id=@Id";
                var result = conn.Query<PriceModel, RentType, Model, PriceModel>(query, (item, rentType, model) =>
                {
                    item.RentType = rentType;
                    item.Model = model;
                    return item;
                }, new { Id = Id }).FirstOrDefault();
                return result;
            }

        }

        public IEnumerable<PriceModel> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = @"select * from PriceModel lrs inner join RentType l on l.Id=lrs.RentTypeId 
                    inner join Model m on lrs.VehicleModelId = m.Id";
                var result = conn.Query<PriceModel, RentType, Model, PriceModel>(query, (item, rentType,model) =>
                {
                    item.RentType = rentType;
                    item.Model = model;
                    return item;
                });
                return result;
            }
        }

        public IEnumerable<PriceModel> GetPriceModelByModel(int modelid)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = @"select * from PriceModel lrs inner join RentType l on l.Id=lrs.RentTypeId where lrs.VehicleModelId = @modelid";
                var result = conn.Query<PriceModel, RentType, PriceModel>(query, (item, rentType) =>
                {
                    item.RentType = rentType;                 
                    return item;
                }, new { ModelId = modelid });
                return result;
            }
        }

        public void Update(PriceModel entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update PriceModel set RentTypeId=@RentTypeId, VehicleModelId=@VehicleModelId, Price=@Price where Id=@Id";
                var result = conn.Query(query, new { RentTypeId = entity.RentTypeId, VehicleModelId = entity.ModelId, Id = entity.Id, Price = entity.Price   });
            }
        }
        public IEnumerable<PriceModel> GetAllPriceModelsByModelId(int modelId)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = @"select * from PriceModel lrs inner join RentType l on l.Id=lrs.RentTypeId 
                    inner join Model m on lrs.VehicleModelId = m.Id where lrs.VehicleModelId=@ModelId";
                var result = conn.Query<PriceModel, RentType, Model, PriceModel>(query, (item, rentType, model) =>
                {
                    item.RentType = rentType;
                    item.Model = model;
                    return item;
                },new { ModelId=modelId});
                return result;
            }
        }

        public IEnumerable<PriceModel> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }
    }
}
