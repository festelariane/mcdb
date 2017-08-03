using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data
{
    public static class DapperExtensions
    {
        public static IEnumerable<TParent> QueryParentChild<TParent, TChild, TParentKey>(this IDbConnection connection,string sql,
        Func<TParent, TParentKey> parentKeySelector,
        Func<TParent, IList<TChild>> childSelector,
        object param = null, IDbTransaction transaction = null, 
        bool buffered = true, string splitOn = "Id",
        int? commandTimeout = null, CommandType? commandType = null)
        {
            Dictionary<TParentKey, TParent> cache = new Dictionary<TParentKey, TParent>();
            connection.Query<TParent, TChild, TParent>(sql, (parent, child) => {
                if(!cache.ContainsKey(parentKeySelector(parent)))
                {
                    cache.Add(parentKeySelector(parent), parent);
                }
                TParent cachedParent = cache[parentKeySelector(parent)];
                IList<TChild> children = childSelector(cachedParent);
                if(child != null)
                    children.Add(child);
                return cachedParent;
            },param,transaction, buffered,splitOn,commandTimeout, commandType);
            return cache.Values;
        }
    }
}
