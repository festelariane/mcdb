using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class RoleRepository : BaseRepository,IRoleRepository
    {
        public UserRole Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from UserRole where Id=@Id";
                var result = conn.QueryFirstOrDefault<UserRole>(query, new { Id = Id });
                return result;
            }
        }

        public IEnumerable<UserRole> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from UserRole";
                var result = conn.Query<UserRole>(query);
                return result;
            }
        }

        public void Add(UserRole entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "insert into UserRole (Name,Guid,CreatedOn,UpdatedOn,IsDeleted) values (@Name,@Guid,@CreatedOn,@UpdatedOn,@IsDeleted)";
                var result = conn.Query(query, new {Name = entity.Name,Guid=Guid.NewGuid(),CreatedOn = DateTime.Now,UpdatedOn=DateTime.Now,IsDeleted=false });
            }
        }

        public void Delete(UserRole entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete UserRole where Id=@Id";
                var result = conn.Query(query, new { Id = entity.Id });
            }
        }

        public void Update(UserRole entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "update UserRole set Name=@Name,UpdatedOn=@UpdatedOn,IsDeleted=@IsDeleted where Id=@Id";
                var result = conn.Execute(query, new { Name = entity.Name,UpdatedOn=DateTime.Now,IsDeleted=entity.IsDeleted, Id = entity.Id });
            }
        }

        public IEnumerable<UserRole> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }
        public bool AssignUserToRole(User user, UserRole role)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var sb = new StringBuilder();
                
                sb.Append("if not exists(select * from [UsersToRoles] with (updlock, serializable) where UserId = @UserId and UserRoleId=@UserRoleId) ");
                sb.Append("insert into [UsersToRoles](UserId, UserRoleId ) values (@UserId, @UserRoleId) ");
                
                var result = conn.Execute(sb.ToString(), new { UserId = user.Id, UserRoleId = role.Id});
                return true;
            }
        }
        public bool RemoveUserRoles(User user, List<int> roleIdList)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var sb = new StringBuilder();
                sb.AppendFormat("delete from UsersToRoles where Userid=@UserId and UserRoleId in ({0})", string.Join(",", roleIdList));

                var result = conn.Execute(sb.ToString(), new { UserId = user.Id });
                return true;
            }
        }
    }
}
