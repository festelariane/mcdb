using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Contract
{
    public interface ISettingRepository : IGenericRepository<Setting>
    {
        IEnumerable<Setting> Find(Expression<Func<Setting, bool>> predicate);
        IEnumerable<Setting> Find(string name, string value);
    }
}
