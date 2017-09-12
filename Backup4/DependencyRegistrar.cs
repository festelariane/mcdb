using DryIoc;
using Mercedes.Core.Configuration;
using Mercedes.Core.DryIoc;
using Mercedes.Core.Infrastructure;
using Mercedes.Plugins.TrinhMinh.WebApi.Data;
using Mercedes.Plugins.TrinhMinh.WebApi.Services;

namespace Mercedes.Plugins.TrinhMinh.WebApi
{
    public class DependencyRegistrar : DryIocModule
    {
        protected override void Load(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config)
        {
            //Register for Data Repositories
            builder.Register<IProjectTypeRepository, ProjectTypeRepository>(Reuse.InWebRequest);
            builder.Register<IProjectRepository, ProjectRepository>(Reuse.InWebRequest);

            //Register for Data Services
            builder.Register<ITmcMainService, TmcMainService>(Reuse.InWebRequest);
        }
    }
}
