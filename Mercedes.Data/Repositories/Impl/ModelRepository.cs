using Mercedes.Data.Repositories.Contract;
using System.Linq;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;
using System;

namespace Mercedes.Data.Repositories.Impl
{
    public class ModelRepository : BaseRepository, IModelRepository
    {
        public Model Get(int Id)
        {
            try {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "select * from Model lrs inner join Category l on l.Id=lrs.CategoryId where lrs.Id=@Id";
                    var result = conn.Query<Model, Category, Model>(query, (item, category) =>
                    {
                        item.Category = category;
                        return item;
                    }, new { Id = Id }).FirstOrDefault();
                    return result;
                }
            } catch { }

            return new Model();
        }

        public IEnumerable<Model> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Model lrs inner join Category l on l.Id=lrs.CategoryId";
                var result = conn.Query<Model, Category, Model>(query, (item, category) =>
                {
                    item.Category = category;
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
                var query = "insert into Model (CategoryID,Code,Name,Published,DisplayOrder,CreatedOn,UpdatedOn) values (@CategoryID,@Code,@Name,@Published,@DisplayOrder,@CreatedOn,@UpdatedOn)";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name, CategoryID = entity.CategoryId, Published = entity.Published, DisplayOrder = entity.DisplayOrder, CreatedOn = DateTime.Now, UpdatedOn = DateTime.Now});
            }
        }

        public void Delete(Model entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update Model set Deleted=1 where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(Model entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update Model set CategoryID=@CategoryId, Code=@Code, Name=@Name,Published=@Published, DisplayOrder=@DisplayOrder,UpdatedOn=@UpdatedOn where Id=@Id";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name, Id = entity.Id, CategoryId = entity.CategoryId, Published = entity.Published, DisplayOrder  = entity.DisplayOrder, UpdatedOn = DateTime.Now});
            }
        }

        public IEnumerable<Model_Image_Mapping> GetVehicleModelImageUrl(int vehicleModelId)
        {           
            try
            {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "select * from Model_Image_Mapping where VehicleModelId=@vehicleModelId order by DisplayOrder";
                    var result = conn.Query<Model_Image_Mapping>(query, new { VehicleModelId = vehicleModelId });
                    return result;
                }
            }
            catch (System.Exception ex){ }

            return null;
        }

        public IEnumerable<Model> GetByCategoryId(int categoryId)
        {
            try {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "select * from Model m left join PriceModel P on m.Id=p.VehicleModelId";                   
                    query += " left join RentType r on r.Id=p.RentTypeId where m.CategoryId=@CategoryId and r.RentTypeSystemName= 'ByMonth'";

                    var result = conn.Query<Model, PriceModel, RentType, Model>(query, (item, priceModel, rentType) =>
                    {
                        item.RentType = rentType;
                        item.PriceModel = priceModel;                      
                        return item;
                    }, new { CategoryId = categoryId });
                    
                    return result;
                }
            } catch { }

            return new List<Model>();
        }

        public Model GetModelDetail(int id)
        {
            try {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "select * from Model m left join PriceModel P on m.Id=p.VehicleModelId";
                    query += " left join RentType r on r.Id=p.RentTypeId where m.Id=@id";

                    var result = conn.Query<Model, PriceModel, RentType, Model>(query, (item, priceModel, rentType) =>
                    {
                        item.RentType = rentType;
                        item.PriceModel = priceModel;
                        return item;
                    }, new { Id = id }).FirstOrDefault();
                   
                    return result;
                }
            } catch { }

            return new Model();
        }

        public void AddModelImage(Model_Image_Mapping entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into [Model_Image_Mapping] ([VehicleModelId],[FullImageUrl],[ThumbImageUrl],[DisplayOrder]) values (@VehicleModelId,@FullImageUrl,@ThumbImageUrl,@DisplayOrder)";
                var result = conn.Query(query, new { VehicleModelId = entity.VehicleModelId, FullImageUrl = entity.FullImageUrl, ThumbImageUrl = entity.ThumbImageUrl, DisplayOrder = entity.DisplayOrder });
            }
        }
    }
}
