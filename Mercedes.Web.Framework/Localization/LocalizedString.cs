using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercedes.Framework.Localization
{
    public class LocalizedString : MarshalByRefObject, IHtmlString
    {
        private readonly string _localized;
        public LocalizedString(string localized)
        {
            _localized = localized;
        }
        public string ToHtmlString()
        {
            return _localized;
        }
    }
}
