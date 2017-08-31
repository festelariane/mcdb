using DryIoc;
using Mercedes.Core;
using Mercedes.Core.Caching;
using Mercedes.Core.Configuration;
using Mercedes.Core.DryIoc;
using Mercedes.Core.Fakes;
using Mercedes.Core.Infrastructure;
using Mercedes.Core.Plugins;
using Mercedes.Framework.UI;
using Mercedes.Services.Contract;
using Mercedes.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercedes.Framework
{
    public class DependencyRegistrar : DryIocModule
    {
        protected override void Load(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config)
        {
            foreach (var type in typeFinder.FindClassesOfType<ISettings>())
            {
                builder.RegisterDelegate(type, (c) =>
                {
                    ISettingService serviceCtx = c.Resolve<ISettingService>();
                    var method = serviceCtx.GetType().GetMethod("LoadSetting").MakeGenericMethod(type);
                    return method.Invoke(serviceCtx, null);
                }, Reuse.InWebRequest);
            }

            builder.Register<IPageHeadBuilder, PageHeadBuilder>(Reuse.InWebRequest);

            builder.RegisterDelegate<HttpContextBase>(c => {
                return HttpContext.Current != null ? (new HttpContextWrapper(HttpContext.Current) as HttpContextBase) : (new FakeHttpContext("~/") as HttpContextBase);
            },Reuse.InResolutionScope);
            
            builder.RegisterDelegate<HttpRequestBase>(c => c.Resolve<HttpContextBase>().Request,Reuse.InResolutionScope);

            builder.RegisterDelegate<HttpResponseBase>(c => c.Resolve<HttpContextBase>().Response, Reuse.InResolutionScope);

            builder.RegisterDelegate<HttpServerUtilityBase>(c => c.Resolve<HttpContextBase>().Server, Reuse.InResolutionScope);

            builder.RegisterDelegate<HttpSessionStateBase>(c => c.Resolve<HttpContextBase>().Session, Reuse.InResolutionScope);

            builder.Register<IWebHelper,WebHelper>(Reuse.Singleton);
            builder.Register<ICacheManager, MemoryCacheManager>(Reuse.Singleton);
            builder.Register<IRouteRegistrator, RouteRegistrator>(Reuse.Singleton);
            builder.Register<IPluginFinder, PluginFinder>(Reuse.Singleton);

        }
    }
}
