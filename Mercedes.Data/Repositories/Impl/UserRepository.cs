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
    public class UserRepository : BaseRepository, IUserRepository
    {
        public User Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select u.*,r.* from [User] u left join [UsersToRoles] ur on u.Id = ur.UserId left join [UserRole] r on ur.UserRoleId = r.Id where u.Id=@Id";
                var result = conn.QueryParentChild<User, UserRole, int>(query, u => u.Id, u => u.Roles, new { Id=Id}).FirstOrDefault();
                return result;
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select u.*,r.* from [User] u left join [UsersToRoles] ur on u.Id = ur.UserId left join [UserRole] r on ur.UserRoleId = r.Id";
                var result = conn.QueryParentChild<User, UserRole, int>(query, u => u.Id, u => u.Roles);
                return result;
            }
        }

        public void Add(User entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into [User] ([Username],[FirstName],[LastName],[Password],[PasswordSalt],[PasswordFormatId],[Email],[IsActive],[LastLoginDate],[LoginAttempts],[UserGuid],[CreatedOn],[UpdatedOn],[IsDeleted]) values (@UserName,@FirstName,@LastName,@Password,@PasswordSalt,@PasswordFormatId,@Email,@IsActive,@LastLoginDate,@LoginAttempts,@UserGuid, @CreatedOn,@UpdatedOn,@IsDeleted)";
                query += " select SCOPE_IDENTITY()";
                var result = conn.ExecuteScalar<int>(query, new
                {
                    UserName = entity.UserName,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Password = entity.Password,
                    PasswordSalt = entity.PasswordSalt,
                    PasswordFormatId = entity.PasswordFormatId,
                    Email = entity.Email,
                    IsActive = entity.IsActive,
                    LastLoginDate = entity.LastLoginDate,
                    LoginAttempts = entity.LoginAttempts,
                    UserGuid = entity.UserGuid,
                    CreatedOn = entity.CreatedOn,
                    UpdatedOn = entity.UpdatedOn,
                    IsDeleted = entity.IsDeleted
                });
                entity.Id = result;
            }
        }

        public void Delete(User entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete [User] where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(User entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update [User] set FirstName=@FirstName,LastName=@LastName,IsActive=@IsActive,UpdatedOn=@UpdatedOn where Id=@Id";
                var result = conn.Query(query, new
                {
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    IsActive = entity.IsActive,
                    UpdatedOn = entity.UpdatedOn,
                    Id = entity.Id
                });
            }
        }

        public IEnumerable<User> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }
    }
}
