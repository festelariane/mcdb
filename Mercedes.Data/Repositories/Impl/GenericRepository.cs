using Dapper;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Impl
{
    public abstract class GenericRepository<TEntity> : BaseRepository, IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly string _tableName;

        public GenericRepository(string tableName)
        {
            _tableName = tableName;
        }
        internal virtual dynamic Mapping(TEntity item)
        {
            return item;
        }
        public virtual void Add(TEntity entity)
        {
            using (var conn = CreateConnection())
            {
                var parameters = (object)Mapping(entity);
                conn.Open();
                entity.Id = conn.Insert<int>(_tableName, parameters);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                conn.Execute("DELETE FROM " + _tableName + " WHERE ID=@ID", new { ID = entity.Id });
            }
        }

        public virtual void Update(TEntity entity)
        {
            using (var conn = CreateConnection())
            {
                var parameters = (object)Mapping(entity);
                conn.Open();
                conn.Update(_tableName, parameters);
            }
        }

        public virtual TEntity Get(int Id)
        {
            TEntity item = default(TEntity);
            using (var conn = CreateConnection())
            {
                conn.Open();
                item = conn.Query<TEntity>("SELECT * FROM " + _tableName + " WHERE ID=@ID", new { ID = Id }).SingleOrDefault();
            }

            return item;
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            IEnumerable<TEntity> items = null;

            using (var conn = CreateConnection())
            {
                conn.Open();
                items = conn.Query<TEntity>("SELECT * FROM " + _tableName);
            }

            return items;
        }
        public virtual IEnumerable<TEntity> GetAllExceptDeletedItems()
        {
            return GetAll();
        }
        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            IEnumerable<TEntity> items = null;
            QueryResult result = DynamicQuery.GetDynamicQuery(_tableName, predicate);
            using (var conn = CreateConnection())
            {
                conn.Open();
                items = conn.Query<TEntity>(result.Sql, (object)result.Param);
            }

            return items;
        }
    }
}
