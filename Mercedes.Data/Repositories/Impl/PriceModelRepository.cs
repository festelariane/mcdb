using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using Dapper;
using Mercedes.Core.Domain;

namespace Mercedes.Data.Repositories.Impl
{
   public class PriceModelRepository : BaseRepository, IPriceModelRepository
    {
        public void Add(PriceModel entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(PriceModel entity)
        {
            throw new NotImplementedException();
        }

        public PriceModel Get(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PriceModel> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = @"select * from PriceModel lrs inner join RentType l on l.Id=lrs.RentTypeId 
                    inner join Model m on lrs.VehicleModelId = m.Id";
                var result = conn.Query<PriceModel, RentType, Model, PriceModel>(query, (item, rentType,model) =>
                {
                    item.RentType = rentType;
                    item.Model = model;
                    return item;
                });
                return result;
            }
        }

        public void Update(PriceModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
