using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class LanguageRepository : BaseRepository, ILanguageRepository
    {
        public void Add(Language entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into Language ([Name],[Culture],[Active],[IsDefault]) values (@Name,@Culture,@Active,@IsDefault)";
                var result = conn.Query(query, new
                {
                    Name = entity.Name,
                    Culture = entity.Culture,
                    Active = entity.Active,
                    IsDefault = entity.IsDefault
                });
            }
        }

        public void Delete(Language entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete from [Language] where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public Language Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Language where Id=@Id";
                var result = conn.Query<Language>(query, new { Id = Id }).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<Language> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Language";
                return conn.Query<Language>(query);
            }
        }

        public IEnumerable<Language> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }

        public void Update(Language entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update [Language] set Name=@Name, Culture=@Culture,Active=@Active, IsDefault=@IsDefault where Id=@Id";
                var result = conn.Query(query, new
                {
                    Name = entity.Name,
                    Culture = entity.Culture,
                    Active = entity.Active,
                    IsDefault= entity.IsDefault,
                    Id = entity.Id
                });
            }
        }
    }
}
