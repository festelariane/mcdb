using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.DryIoc
{
    public abstract class DryIocModule
    {
        protected abstract void Load(IRegistrator builder);
        public void Configure(IRegistrator builder)
        {
            Load(builder);
        }
    }
}
