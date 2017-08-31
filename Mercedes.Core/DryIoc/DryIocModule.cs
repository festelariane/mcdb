using DryIoc;
using Mercedes.Core.Configuration;
using Mercedes.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.DryIoc
{
    public abstract class DryIocModule
    {
        protected abstract void Load(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config);
        public void Configure(IRegistrator builder, ITypeFinder typeFinder, SiteConfig config)
        {
            Load(builder, typeFinder, config);
        }
    }
}
