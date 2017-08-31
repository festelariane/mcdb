using Mercedes.Core.Domain.Settings;
using Mercedes.Core.Infrastructure;
using Mercedes.Web.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Web.Controllers
{
    public class CommonController : Controller
    {
        public CommonController()
        { }
        [ChildActionOnly]
        public virtual ActionResult Social()
        {
            var socialSetting = IocHelper.Resolve<SocialSetting>();
            var model = new SocialModel()
            {
                FacebookLink = socialSetting.FacebookLink,
                GooglePlusLink = socialSetting.GooglePlusLink,
                TwitterLink = socialSetting.TwitterLink,
                YoutubeLink = socialSetting.YoutubeLink
            };
            return PartialView(model);
        }
    }
}