using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mercedes.Framework.Localization
{
    public interface ILocalizedModel
    {
    }
    public interface ILocalizedModel<TLocalizedModel> : ILocalizedModel where TLocalizedModel : ILocalizedModelLocal
    {
        IList<TLocalizedModel> Locales { get; set; }
    }
}