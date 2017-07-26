using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;

namespace Mercedes.Services.Impl
{
    public class SettingService : ISettingService
    {
        private readonly ISettingRepository _settingRepository;
        public SettingService(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public bool AddOrUpdate(Setting entity)
        {
            try
            {
                if (entity.Id > 0)
                {
                    _settingRepository.Update(entity);
                }
                else
                {
                    _settingRepository.Add(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Setting entity)
        {
            _settingRepository.Delete(entity);
            return true;
        }
        public IList<Setting> GetAllSettings()
        {
            return FindSettings(null, null);
        }

        public Setting GetSettingById(int Id)
        {
            return _settingRepository.Get(Id);
        }
        public IList<Setting> FindSettings(string name, string value)
        {
            var result = _settingRepository.Find(name, value).ToList();
            return result;
        }
    }
}
