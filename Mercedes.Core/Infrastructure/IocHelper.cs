using DryIoc;
using Mercedes.Core.Infrastructure.DependencyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Infrastructure
{
    public class IocHelper
    {
        private static ContainerManager _containerManager;
        public static void Initialize(IContainer container)
        {
            _containerManager = new ContainerManager(container);
        }
        public static T Resolve<T>(string key = "") where T : class
        {
            return _containerManager.Resolve<T>(key);
        }
        public static T Resolve<T>() where T : class
        {
            return _containerManager.Resolve<T>("", null);
        }

        public static object Resolve(Type type)
        {
            return _containerManager.Resolve(type);
        }

        public static T[] ResolveAll<T>()
        {
            return _containerManager.ResolveAll<T>();
        }
    }
}
