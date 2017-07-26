using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface ISettingService
    {
        bool AddOrUpdate(Setting entity);
        bool Delete(Setting entity);
        Setting GetSettingById(int Id);
        IList<Setting> GetAllSettings();
        IList<Setting> FindSettings(string name, string value);
    }
}
