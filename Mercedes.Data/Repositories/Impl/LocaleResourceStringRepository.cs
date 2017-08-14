using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Impl
{
    public class LocaleResourceStringRepository : BaseRepository, ILocaleResourceStringRepository
    {
        public Core.Domain.LocaleResourceString Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Core.Domain.LocaleResourceString> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Core.Domain.LocaleResourceString> GetAllExceptDeletedItems()
        {
            throw new NotImplementedException();
        }

        public void Add(Core.Domain.LocaleResourceString entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Core.Domain.LocaleResourceString entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Core.Domain.LocaleResourceString entity)
        {
            throw new NotImplementedException();
        }
    }
}
