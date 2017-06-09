

using Mercedes.Framework.Localization;
using System.IO;
using System.Web.Mvc;

namespace Mercedes.Framework.Mvc.Pages
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        public override void InitHelpers()
        {
            base.InitHelpers();
        }
        public override string Layout
        {
            get
            {
                var layout = base.Layout;
                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = System.Web.Mvc.ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }

        private Localizer _localizer;
        public Localizer T
        {
            get
            {
                if(_localizer == null)
                {
                    _localizer = (key, args) => {
                        var resString = "";// _resourceService.GetResourceString(key);
                        if(string.IsNullOrEmpty(resString))
                        {
                            return new LocalizedString(key);
                        }
                        if(args != null && args.Length > 0)
                        {
                            resString = string.Format(resString, args);
                        }
                        return new LocalizedString(resString);
                    };
                }
                return _localizer;
            }
        }
    }
}
