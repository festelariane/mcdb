using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Contract
{
    public interface ILocaleResourceStringRepository : IGenericRepository<LocaleResourceString>
    {
        IEnumerable<LocaleResourceString> GetLocaleResourceStringsByLang(int lang);
        IEnumerable<LocaleResourceString> GetLocaleResourceStringsByKey(string key);
        bool AddLocaleResourceString(List<LocaleResourceString> resources);
        bool UpdateLocaleResourceString(List<LocaleResourceString> resources);
    }
}
