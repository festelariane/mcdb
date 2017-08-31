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
using Mercedes.Services.Contract.Security;
using Mercedes.Services.Impl.Security;
using Mercedes.Core.Configuration;
using Mercedes.Core.Infrastructure;

namespace Mercedes.Services
{
    public class DependencyRegistrar : DryIocModule
    {
        protected override void Load(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config)
        {
            //Register for Data Repositories
            builder.Register<IVehicleRepository, VehicleRepository>(Reuse.InWebRequest);
            builder.Register<IRoleRepository, RoleRepository>(Reuse.InWebRequest);
            builder.Register<IUserRepository, UserRepository>(Reuse.InWebRequest);
            builder.Register<IManufacturerRepository, ManufacturerRepository>(Reuse.InWebRequest);
            builder.Register<IModelRepository, ModelRepository>(Reuse.InWebRequest);
            builder.Register<IRentTypeRepository, RentTypeRepository>(Reuse.InWebRequest);
            builder.Register<IPriceModelRepository, PriceModelRepository>(Reuse.InWebRequest);
            builder.Register<ICategoryRepository, CategoryRepository>(Reuse.InWebRequest);
            builder.Register<IContactRepository, ContactRepository>(Reuse.InWebRequest);
            builder.Register<ISettingRepository, SettingRepository>(Reuse.InWebRequest);
            builder.Register<IEmailAccountRepository, EmailAccountRepository>(Reuse.InWebRequest);

            //Register for Data Services
            builder.Register<ICarService, CarService>(Reuse.Transient);
            builder.Register<IRentService, RentService>(Reuse.Transient);
            builder.Register<IContactService, ContactService>(Reuse.Transient);
            builder.Register<IRoleService, RoleService>(Reuse.InWebRequest);
            builder.Register<IUserManagementService, UserManagementService>(Reuse.InWebRequest);
            builder.Register<IPictureService, PictureService>(Reuse.InWebRequest);
            builder.Register<ISettingService, SettingService>(Reuse.InWebRequest);
            builder.Register<IEncryptionService, EncryptionService>(Reuse.Singleton);
            builder.Register<IUserRegistrationService, UserRegistrationService>(Reuse.InWebRequest);
            builder.Register<IEmailService, EmailService>(Reuse.Singleton);
            builder.Register<IEmailAccountService, EmailAccountService>(Reuse.InWebRequest);
        }
    }
}
