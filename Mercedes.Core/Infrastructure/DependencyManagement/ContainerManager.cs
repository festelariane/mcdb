using DryIoc;
using DryIoc.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercedes.Core.Infrastructure.DependencyManagement
{
    public class ContainerManager
    {
        private readonly IContainer _container;

        public ContainerManager(IContainer container)
        {
            this._container = container;
        }

        public virtual IContainer Container
        {
            get
            {
                return _container;
            }
        }

        public virtual T Resolve<T>(string key = "", IContainer scope = null) where T : class
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<T>();
            }
            return scope.Resolve<T>(key);
        }

        public virtual object Resolve(Type type, IContainer scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            return scope.Resolve(type);
        }

        public virtual T[] ResolveAll<T>(string key = "", IContainer scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            if (string.IsNullOrEmpty(key))
            {
                return scope.Resolve<IEnumerable<T>>().ToArray();
            }
            return scope.Resolve<IEnumerable<T>>(key).ToArray();
        }

        public virtual bool IsRegistered(Type serviceType, IContainer scope = null)
        {
            if (scope == null)
            {
                //no scope specified
                scope = Scope();
            }
            
            return scope.IsRegistered(serviceType);
        }

        public virtual IContainer Scope()
        {
            return Container;
            //try
            //{
            //    if (Container.ScopeContext.GetCurrentOrDefault() == null)
            //    {
            //        if (HttpContext.Current != null)
            //            return Container.OpenScope(Reuse.WebRequestScopeName);
            //        return Container.OpenScope(Reuse.InResolutionScope); 
            //    }
            //    return Container;
            //}
            //catch (Exception ex)
            //{
            //    return Container.OpenScope();
            //}
        }
    }
}
