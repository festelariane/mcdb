using DryIoc;
using Mercedes.Core.Configuration;
using Mercedes.Core.DryIoc;
using Mercedes.Core.Infrastructure;
using Mercedes.Plugins.TrinhMinh.WebApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Plugins.TrinhMinh.WebApi
{
    public class DependencyRegistrar : DryIocModule
    {
        protected override void Load(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config)
        {
        }
    }
}
