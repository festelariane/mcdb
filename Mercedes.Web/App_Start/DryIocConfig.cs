using DryIoc;
using DryIoc.Mvc;
using DryIoc.Web;
using Mercedes.Core.Configuration;
using Mercedes.Core.DryIoc;
using Mercedes.Core.Infrastructure;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Http;
using Owin;
using Mercedes.Web.App_Start;

namespace Mercedes.Web
{
    public class DryIocConfig
    {
        public static void BuildConfig(IAppBuilder app)
        {
            IContainer container = new Container();
            // Enable basic MVC support. 
            container = container.WithMvc(

                // optional: enable original DryIoc exceptions when resolving controllers - provides more info if resolve is failed
                throwIfUnresolved: type => type.IsController(),

                // optional: provide the scope context with User error handler to find why request scope is not created / disposed
                scopeContext: new HttpContextScopeContext(ex => { })
            );
            
            //var typeFinder = new WebAppTypeFinder();
            //container.RegisterInstance<ITypeFinder>(typeFinder, Reuse.Singleton);
            //container.RegisterMany(AppDomain.CurrentDomain.GetAssemblies(), serviceTypeCondition: type => type == typeof(DryIocModule));
            //foreach (var module in container.ResolveMany<DryIocModule>())
            //    module.Configure(container);
            //foreach(var type in typeFinder.FindClassesOfType<ISettings>())
            //{
            //    container.RegisterDelegate(type, (c) => 
            //    {
            //        ISettingService serviceCtx = c.Resolve<ISettingService>();
            //        var method = serviceCtx.GetType().GetMethod("LoadSetting").MakeGenericMethod(type);
            //        return method.Invoke(serviceCtx, null);
            //        //return c.Resolve<ISettingService>().GetType().GetMethod("LoadSetting").MakeGenericMethod(type).Invoke(null,null);
            //    },Reuse.InWebRequest);
            //}

            //container.RegisterMany(typeFinder.GetAssemblies(), serviceTypeCondition: type => type.IsAssignableTo(typeof(ISettings)),
            //    reuse: Reuse.InWebRequest);
            WebApiConfig.Configure(app, container);
    
            IocHelper.Initialize(container);
            
        }
    }
}