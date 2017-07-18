using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Contract
{
    public interface IModelRepository : IGenericRepository<Model>
    {
        IEnumerable<Model> GetByCategoryId(int categoryId);
        IEnumerable<Model_Image_Mapping> GetVehicleModelImageUrl(int vehicleModelId);
        void AddModelImage(Model_Image_Mapping entity);
        void DeleteModelImage(Model_Image_Mapping entity);
    }
}
