using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Contract
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        TEntity Get(int Id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllExceptDeletedItems();
        void Add(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
        //IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

    }
}
