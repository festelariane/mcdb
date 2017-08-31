using DryIoc;
using DryIoc.Mvc;
using DryIoc.Web;
using Mercedes.Core.Configuration;
using Mercedes.Core.DryIoc;
using Mercedes.Core.Infrastructure.DependencyManagement;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Mercedes.Core.Infrastructure
{
    public sealed class SiteEngine
    {
        private static readonly Lazy<SiteEngine> lazy = new Lazy<SiteEngine>(
            ()=> {
                    var engine = new SiteEngine();
                    engine.Init();
                    return engine;
                } 
            );
        public static SiteEngine Instance
        {
            get
            {
                return lazy.Value;
            }
        }
        private SiteEngine()
        {

        }
        private bool _initialized = false;
        private ContainerManager _containerManager;
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void Init()
        {
            if(!_initialized)
            {
                RegisterDependencies();

                _initialized = true;
            }
        }

        private void RegisterDependencies()
        {
            IContainer container = new Container();
            // Enable basic MVC support. 
            container = container.WithMvc(

                // optional: enable original DryIoc exceptions when resolving controllers - provides more info if resolve is failed
                throwIfUnresolved: type => type.IsController(),

                // optional: provide the scope context with User error handler to find why request scope is not created / disposed
                scopeContext: new HttpContextScopeContext(ex => { })
            );

            var typeFinder = new WebAppTypeFinder();
            var siteConfig = new SiteConfig();
            container.RegisterInstance<ITypeFinder>(typeFinder, Reuse.Singleton);
            
            container.RegisterMany(typeFinder.GetAssemblies(), serviceTypeCondition: type => type == typeof(DryIocModule));
            foreach (var module in container.ResolveMany<DryIocModule>())
                module.Configure(container, typeFinder, siteConfig);
            _containerManager = new ContainerManager(container);
            
            IocHelper.Initialize(container);
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }
        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }
        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }
        public ContainerManager ContainerManager
        {
            get { return _containerManager; }
        }
    }
}
