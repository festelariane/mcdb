using Mercedes.Core.DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DryIoc;
using Mercedes.Data.Repositories.Contract;
using Mercedes.Data.Repositories.Impl;
using Mercedes.Services.Contract;
using Mercedes.Services.Impl;
namespace Mercedes.Services
{
    public class DependencyRegistrar : DryIocModule
    {
        protected override void Load(IRegistrator builder)
        {
            //Registera for Data Repositories
            builder.Register<IVehicleRepository, VehicleRepository>(Reuse.InWebRequest);
            builder.Register<IRoleRepository, RoleRepository>(Reuse.InWebRequest);
            builder.Register<IUserRepository, UserRepository>(Reuse.InWebRequest);
            builder.Register<IManufacturerRepository, ManufacturerRepository>(Reuse.InWebRequest);
            builder.Register<IModelRepository, ModelRepository>(Reuse.InWebRequest);

            //Registera for Data Services
            builder.Register<ICarService, CarService>(Reuse.Transient);
            builder.Register<IRoleService, RoleService>(Reuse.InWebRequest);
            builder.Register<IUserManagementService, UserManagementService>(Reuse.InWebRequest);
        }
    }
}
