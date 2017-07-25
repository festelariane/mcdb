using Mercedes.Data.Repositories.Contract;
using System.Linq;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public Category Get(int Id)
        {
            try {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "select * from Category lrs inner join Manufacturer l on l.Id=lrs.ManufacturerId where lrs.Id=@Id";
                    var result = conn.Query<Category, Manufacturer, Category>(query, (item, manufacturer) =>
                    {
                        item.Manufacturer = manufacturer;
                        return item;
                    }, new { Id = Id }).FirstOrDefault();
                    return result;
                }
            } catch { }

            return new Category();
        }

        public IEnumerable<Category> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Category lrs inner join Manufacturer l on l.Id=lrs.ManufacturerId where Published=1";
                var result = conn.Query<Category, Manufacturer, Category>(query, (item, manufacturer) =>
                {
                    item.Manufacturer = manufacturer;
                    return item;
                });
                return result;
            }
        }

        public void Add(Category entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into Category (ManufacturerId,Code,Name,ImageUrl,ImageUrl_2,Published,Deleted,DisplayOrder,CreatedOn,UpdatedOn) values (@ManufacturerId,@Code,@Name,@ImageUrl,@ImageUrl_2,@Published,@Deleted,@DisplayOrder,@CreatedOn,@UpdatedOn)";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name,ManufacturerId=entity.ManufacturerId, ImageUrl = entity.ImageUrl, ImageUrl_2 = entity.ImageUrl_2, Published = entity.Published,
                    Deleted = entity.Deleted, DisplayOrder = entity.DisplayOrder, CreatedOn = entity.CreatedOn, UpdatedOn=entity.UpdatedOn });
            }
        }

        public void Delete(Category entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete Category where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(Category entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update Category set ManufacturerId=@ManufacturerId, Code=@Code, Name=@Name, ImageUrl=@ImageUrl,ImageUrl_2=@ImageUrl_2, Published=@Published,Deleted=@Deleted,DisplayOrder=@DisplayOrder, CreatedOn=@CreatedOn, UpdatedOn=@UpdatedOn where Id=@Id";
                var result = conn.Query(query, new { Code = entity.Code, Name = entity.Name, Id = entity.Id,ManufacturerId=entity.ManufacturerId,
                ImageUrl=entity.ImageUrl,ImageUrl_2=entity.ImageUrl_2,Published=entity.Published,Deleted=entity.Deleted,DisplayOrder=entity.DisplayOrder,
                    CreatedOn = entity.CreatedOn,
                    UpdatedOn = entity.UpdatedOn});
            }
        }
    }
}
