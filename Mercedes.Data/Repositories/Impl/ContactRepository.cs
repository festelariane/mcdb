using Mercedes.Data.Repositories.Contract;
using System.Linq;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class ContactRepository : BaseRepository, IContactRepository
    {
        public Contact Get(int Id)
        {
            try {
                using (var conn = CreateConnection())
                {
                    conn.Open();                                 
                    var query = "select * from Contact where Id=@Id";
                    var result = conn.QueryFirst<Contact>(query, new { Id = Id });
                    return result;
                }
            } catch { }

            return new Contact();
        }

        public IEnumerable<Contact> GetAll()
        {
            try {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "select top 1000 * from Contact order by [datetime] desc";
                    var result = conn.Query<Contact>(query);
                    return result;
                }
            } catch { }

            return new List<Contact>();
        }

        public void Add(Contact entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into Contact (Name,Email,Title,Message) values (@Name,@Email,@Title,@Message)";
                var result = conn.Query(query, new { Name = entity.Name, Email = entity.Email, Title = entity.Title, Message = entity.Message });
            }
        }

        public void Delete(Contact entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete Contact where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(Contact entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update Category set Name=@Name, Email=@Email, Title=@Title, Message=@Message where Id=@Id";
                var result = conn.Query(query, new { Name = entity.Name, Email = entity.Email, Title = entity.Title, Message = entity.Message });
            }
        }
    }
}
