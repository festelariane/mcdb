﻿using Mercedes.Services.Contract;
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
        private readonly ILanguageRepository _languageRepository;
        public SettingService(ISettingRepository settingRepository,ILanguageRepository languageRespository)
        {
            _settingRepository = settingRepository;
            _languageRepository = languageRespository;
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

        public bool AddOrUpdate(Language entity)
        {
            try
            {
                if (entity.Id > 0)
                {
                    _languageRepository.Update(entity);
                }
                else
                {
                    _languageRepository.Add(entity);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool Delete(Language entity)
        {
            _languageRepository.Delete(entity);
            return true;
        }

        public Language GetLanguageById(int Id)
        {
            return _languageRepository.Get(Id);
        }

        public List<Language> GetAllLanguages()
        {
            return _languageRepository.GetAll().ToList();
        }
    }
}
