using Mercedes.Data.Repositories.Contract;
using System.Linq;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;
using System;
using System.Linq.Expressions;

namespace Mercedes.Data.Repositories.Impl
{
    public class SettingRepository : BaseRepository, ISettingRepository
    {
        public void Add(Setting entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into Setting ([Name],[Value]) values (@Name,@Value)";
                var result = conn.Query(query, new
                {
                    Name = entity.Name,
                    Value = entity.Value
                });
            }
        }

        public void Delete(Setting entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete from [Setting] where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public Setting Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Setting where Id=@Id";
                var result = conn.Query<Setting>(query,new { Id = Id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Setting> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Setting";
                return conn.Query<Setting>(query);
            }
        }

        public IEnumerable<Setting> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }

        public void Update(Setting entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update [Setting] set Name=@Name, Value=@Value where Id=@Id";
                var result = conn.Query(query, new
                {
                    Name = entity.Name,
                    Value = entity.Value,
                    Id = entity.Id
                });
            }
        }
        public IEnumerable<Setting> Find(Expression<Func<Setting, bool>> predicate)
        {
            throw new NotImplementedException("Will implement later");
        }
        public IEnumerable<Setting> Find(string name, string value)
        {
            var builder = new SqlBuilder();
            var template = builder.AddTemplate("SELECT * from Setting ");
            if(!string.IsNullOrWhiteSpace(name))
            {
                builder.Where("Name like @term", new { term = "%" + name + "%" });
            }
            if (!string.IsNullOrWhiteSpace(value))
            {
                builder.Where("Value like @term", new { term = "%" + value + "%" });
            }
            using (var conn = CreateConnection())
            {
                conn.Open();
                var result = conn.Query<Setting>(template.RawSql, template.Parameters);
                return result;
            }
        }
    }
}
