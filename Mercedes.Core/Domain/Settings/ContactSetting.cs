using Mercedes.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain.Settings
{
    public class ContactSetting : ISettings
    {
        public string SkypeUserName { get; set; }
        public string YahooUserName { get; set; }
    }
}