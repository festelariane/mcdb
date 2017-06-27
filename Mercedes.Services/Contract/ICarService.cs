using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface ICarService
    {
        Manufacturer GetManufacturerById(int manufacturerId);
        IList<Manufacturer> GetAllManufacturers();
        bool AddManufacturer(Manufacturer manufacturer);
        bool DeleteManufacturer(Manufacturer manufacturer);
        bool UpdateManufacturer(Manufacturer manufacturer);
        //Model class
        Model GetModelById(int modelId);
        IList<Model> GetAllModel();
        bool AddModel(Model model);
        bool DeleteModel(Model model);
        bool UpdateModel(Model model);
    }
}
