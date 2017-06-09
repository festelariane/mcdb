using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Framework.Localization
{
    public delegate LocalizedString Localizer(string text, params object[] args);
}
