using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;
using Mercedes.Core.Configuration;
using Mercedes.Core.Caching;
using System.ComponentModel;

namespace Mercedes.Services.Impl
{
    public class SettingService : ISettingService
    {
        private const string SETTINGS_ALL_KEY = "MerBenz.setting.all";
        private const string SETTINGS_PATTERN_KEY = "MerBenz.setting.";

        private readonly ISettingRepository _settingRepository;
        private readonly ICacheManager _cacheManager;
        public SettingService(ISettingRepository settingRepository, ICacheManager cacheManager)
        {
            _settingRepository = settingRepository;
            _cacheManager = cacheManager;
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
        protected virtual IList<Setting> GetAllSettingsCached()
        {
            string key = string.Format(SETTINGS_ALL_KEY);
            return _cacheManager.Get(key, () =>
             {
                 return GetAllSettings();
             });
        }
        public T GetSettingByKey<T>(string key, T defaultValue = default(T))
        {
            if (String.IsNullOrEmpty(key))
                return defaultValue;
            var allSettings = GetAllSettingsCached();
            Setting found = null;
            foreach(var setting in allSettings)
            {
                if (string.Equals(setting.Name, key, StringComparison.InvariantCultureIgnoreCase))
                {
                    found = setting;
                    break;
                }
            }
            if(found != null)
            {
                return CodeHelper.To<T>(found.Value);
            }
            return defaultValue;
        }
        public T LoadSetting<T>() where T : ISettings
        {
            string cachedKey = SETTINGS_PATTERN_KEY + typeof(T).Name;
            return _cacheManager.Get(cachedKey, () =>
            {
                var settings = Activator.CreateInstance<T>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    if (!prop.CanRead || !prop.CanWrite)
                        continue;
                    var key = typeof(T).Name + "." + prop.Name;
                    var setting = GetSettingByKey<string>(key);
                    if (setting == null)
                        continue;
                    var typeConverter = TypeDescriptor.GetConverter(prop.PropertyType);

                    if (!typeConverter.CanConvertFrom(typeof(string)))
                        continue;

                    if (!typeConverter.IsValid(setting))
                        continue;
                    object value = typeConverter.ConvertFromInvariantString(setting);
                    prop.SetValue(settings, value, null);
                }
                return settings;
            });
            
        }
    }
}
