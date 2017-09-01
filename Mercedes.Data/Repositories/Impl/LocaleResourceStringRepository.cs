using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Dapper;

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
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "delete from [LocaleResourceString] where ResourceName=@ResourceName";
                var result = conn.Query(query, new { ResourceName = entity.ResourceName });
            }
        }

        public void Update(Core.Domain.LocaleResourceString entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LocaleResourceString> GetLocaleResourceStringsByLang(int lang)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from LocaleResourceString r inner join Language l  on r.LanguageId = l.Id where r.LanguageId=" + lang;
                return conn.Query<LocaleResourceString,Language,LocaleResourceString>(query,(item,lanuage)=> {
                    item.Language = lanuage;
                    return item;
                });
            }
        }

        public IEnumerable<LocaleResourceString> GetLocaleResourceStringsByKey(string key)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from LocaleResourceString r inner join Language l  on r.LanguageId = l.Id where r.ResourceName='" + key+"'";
                return conn.Query<LocaleResourceString, Language, LocaleResourceString>(query, (item, lanuage) => {
                    item.Language = lanuage;
                    return item;
                });
            }
        }

        public bool AddLocaleResourceString(List<LocaleResourceString> resources)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "insert into LocaleResourceString ([LanguageId],[ResourceName],[ResourceValue]) values (@LanguageId,@ResourceName,@ResourceValue)";
                    foreach (var res in resources)
                    {
                        var result = conn.Query(query, new
                        {
                            LanguageId = res.LanguageId,
                            ResourceName = res.ResourceName,
                            ResourceValue = res.ResourceValue
                        });
                    }                    
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }

        public bool UpdateLocaleResourceString(List<LocaleResourceString> resources)
        {
            try
            {
                using (var conn = CreateConnection())
                {
                    conn.Open();
                    var query = "update LocaleResourceString set ResourceValue=@ResourceValue Where LanguageId=@LanguageId and ResourceName=@ResourceName";
                    foreach (var res in resources)
                    {
                        var result = conn.Query(query, new
                        {
                            LanguageId = res.LanguageId,
                            ResourceName = res.ResourceName,
                            ResourceValue = res.ResourceValue
                        });
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
