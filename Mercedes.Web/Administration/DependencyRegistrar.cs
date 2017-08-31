using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DryIoc;
using Mercedes.Core.DryIoc;
using Mercedes.Framework.Menu;
using System.Collections;
using Mercedes.Core.Infrastructure;
using Mercedes.Core.Configuration;

namespace Mercedes.Admin
{
    public class DependencyRegistrar : DryIocModule
    {
        protected override void Load(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config)
        {
            builder.RegisterDelegate<Func<IList<string>, bool>>(c =>
            {
                return roles =>
                {
                    var Session = c.Resolve<HttpSessionStateBase>();
                    return true;
                };
            }, serviceKey: "funcHasRole");
            
            builder.Register<XmlSiteMap>(Made.Of(() => new XmlSiteMap(Arg.Index<string>(0),Arg.Of<Func<IList<string>, bool>>("funcHasRole")),
                request1 => "~/Administration/sitemap.config"),serviceKey: "adminsitemap");
            
            //c.Register<XmlSiteMap>(Reuse.Singleton, made: Made.Of(r => ServiceInfo.Of<Factory>(),
            //            f => f.Create("some value") //How do I pass a string to the factory method?
            //        ));

            //builder.RegisterType<XmlSiteMap>().As<XmlSiteMap>()
            //   .WithParameter("fileName", "~/Administration/sitemap.config")
            //   .WithParameter(new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(Func<IList<string>, bool>), (pi, ctx) => ctx.ResolveNamed("funcHasRole", typeof(Func<IList<string>, bool>))))
            //   .Named<XmlSiteMap>("adminsitemap").InstancePerRequest();
        }
    }
}