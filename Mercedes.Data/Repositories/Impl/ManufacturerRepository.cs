using Mercedes.Data.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Core.Domain;
using Dapper;

namespace Mercedes.Data.Repositories.Impl
{
    public class ManufacturerRepository : BaseRepository, IManufacturerRepository
    {
        public Manufacturer Get(int Id)
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Manufacturer where Id=@Id";
                var result = conn.QueryFirst<Manufacturer>(query, new { Id = Id});
                return result;
            }
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            using (var conn = CreateConnection())
            {
                conn.Open();
                var query = "select * from Manufacturer";
                var result = conn.Query<Manufacturer>(query);
                return result;
            }
        }

        public void Add(Manufacturer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Manufacturer entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Manufacturer entity)
        {
            throw new NotImplementedException();
        }
    }
}
