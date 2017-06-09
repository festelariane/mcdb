using DryIoc;
using DryIoc.Mvc;
using DryIoc.Web;
using Mercedes.Core.DryIoc;
using Mercedes.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web
{
    public class DryIocConfig
    {
        public static void BuildConfig()
        {
            IContainer container = new Container();
            // Enable basic MVC support. 
            container = container.WithMvc(

                // optional: enable original DryIoc exceptions when resolving controllers - provides more info if resolve is failed
                throwIfUnresolved: type => type.IsController(),

                // optional: provide the scope context with User error handler to find why request scope is not created / disposed
                scopeContext: new HttpContextScopeContext(ex => { })
            );
            container.RegisterMany(AppDomain.CurrentDomain.GetAssemblies(), serviceTypeCondition: type => type == typeof(DryIocModule));
            foreach (var module in container.ResolveMany<DryIocModule>())
                module.Configure(container);
            IocHelper.Initialize(container);
        }
    }
}