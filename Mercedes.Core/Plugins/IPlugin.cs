using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Plugins
{
    public interface IPlugin
    {
        PluginDescriptor PluginDescriptor { get; set; }
        void Install();
        void Uninstall();
    }
}
