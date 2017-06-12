using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Mercedes.Web.Framework.Mvc.Infrastructure
{
    public class MercedesViewEngine : ThemeableVirtualPathProviderViewEngine
    {
        public MercedesViewEngine()
        {
            ViewLocationCache = new DefaultViewLocationCache(TimeSpan.FromHours(1));
            AreaViewLocationFormats = new[]
                                        {
                                              
                                              //default
                                              "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                              "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                          };

            AreaMasterLocationFormats = new[]
                                            {
                                               
                                                //default
                                                "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                "~/Areas/{2}/Views/Shared/{0}.cshtml",
                                            };

            AreaPartialViewLocationFormats = new[]
                                                 {
                                                    
                                                    //default
                                                    "~/Areas/{2}/Views/{1}/{0}.cshtml",
                                                    "~/Areas/{2}/Views/Shared/{0}.cshtml"
                                                 };
            ViewLocationFormats = new[]
                                      {
                                           
                                            //default
                                            "~/Views/{1}/{0}.cshtml",
                                            "~/Views/Shared/{0}.cshtml",

                                            //Admin
                                            "~/Administration/Views/{1}/{0}.cshtml",
                                            "~/Administration/Views/Shared/{0}.cshtml",
                                      };
            MasterLocationFormats = new[]
                            {
                                           

                                            //default
                                            "~/Views/{1}/{0}.cshtml",
                                            "~/Views/Shared/{0}.cshtml"
                                        };

            PartialViewLocationFormats = new[]
                                             {
                                               
                                                //default
                                                "~/Views/{1}/{0}.cshtml",
                                                "~/Views/Shared/{0}.cshtml", 

                                                //Admin
                                                "~/Administration/Views/{1}/{0}.cshtml",
                                                "~/Administration/Views/Shared/{0}.cshtml",
                                             };

            FileExtensions = new[] { "cshtml" };
        }
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, partialPath, null, false, fileExtensions);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            IEnumerable<string> fileExtensions = base.FileExtensions;
            return new RazorView(controllerContext, viewPath, masterPath, true, fileExtensions);
        }
    }
}
