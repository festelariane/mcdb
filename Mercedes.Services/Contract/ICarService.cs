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
    }
}
