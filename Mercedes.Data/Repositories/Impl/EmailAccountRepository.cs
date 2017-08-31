using Mercedes.Data.Repositories.Contract;
using System.Linq;
using System.Collections.Generic;
using Mercedes.Core.Domain;
using Dapper;
using System;

namespace Mercedes.Data.Repositories.Impl
{
    public class EmailAccountRepository : BaseRepository, IEmailAccountRepository
    {
        public virtual void Add(EmailAccount entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "INSERT INTO [EmailAccount]([Email],[DisplayName],[Host],[Port],[Username],[Password],[EnableSsl],[UseDefaultCredentials]) VALUES";
                query += "(@Email,@DisplayName,@Host,@Port,@Username,@Password,@EnableSsl,@UseDefaultCredentials)";
                conn.Execute(query, new {
                    Email = entity.Email,
                    DisplayName = entity.DisplayName,
                    Host = entity.Host,
                    Port = entity.Port,
                    Username = entity.Username,
                    Password = entity.Password,
                    EnableSsl = entity.EnableSsl,
                    UseDefaultCredentials = entity.UseDefaultCredentials
                });
            }
        }

        public virtual void Delete(EmailAccount entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM [EmailAccount] WHERE ID=@ID", new { ID = entity.Id });
            }
        }

        public virtual void Update(EmailAccount entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "Update [EmailAccount] set ";
                query += "Email = @Email,DisplayName=@DisplayName,Host=@Host,Port=@Port,Username=@Username,Password=@Password,EnableSsl=@EnableSsl,UseDefaultCredentials=@UseDefaultCredentials WHERE Id=@Id";
                conn.Execute(query, new
                {
                    Id= entity.Id,
                    Email = entity.Email,
                    DisplayName = entity.DisplayName,
                    Host = entity.Host,
                    Port = entity.Port,
                    Username = entity.Username,
                    Password = entity.Password,
                    EnableSsl = entity.EnableSsl,
                    UseDefaultCredentials = entity.UseDefaultCredentials
                });
            }
        }

        public virtual EmailAccount Get(int Id)
        {
            EmailAccount item = default(EmailAccount);
            using (var conn = CreateConnection())
            {
                conn.Open();
                item = conn.Query<EmailAccount>("SELECT * FROM [EmailAccount] WHERE ID=@ID", new { ID = Id }).SingleOrDefault();
            }

            return item;
        }

        public virtual IEnumerable<EmailAccount> GetAll()
        {
            IEnumerable<EmailAccount> items = null;

            using (var conn = CreateConnection())
            {
                conn.Open();
                items = conn.Query<EmailAccount>("SELECT * FROM [EmailAccount]");
            }

            return items;
        }
        public IEnumerable<EmailAccount> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }
    }
}
