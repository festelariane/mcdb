
using Mercedes.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Framework.UI
{
    public static class LayoutExtensions
    {
        public static void AddTitleParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddTitleParts(part);
        }
        public static void AppendTitleParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendTitleParts(part);
        }
        public static MvcHtmlString GenerateTitle(this HtmlHelper html, bool addDefaultTitle, string part = "")
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            if(!string.IsNullOrEmpty(part))
            {
                pageHeadBuilder.AddTitleParts(part);
            }
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateTitle(addDefaultTitle)));
        }
        public static void AddMetaDescriptionParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddMetaDescriptionParts(part);
        }
        public static void AppendMetaDescriptionParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendMetaDescriptionParts(part);
        }
        public static MvcHtmlString GenerateMetaDescription(this HtmlHelper html, string part = "")
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            html.AppendMetaDescriptionParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateMetaDescription()));
        }

        public static void AddMetaKeywordParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddMetaKeywordParts(part);
        }
        public static void AppendMetaKeywordParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendMetaKeywordParts(part);
        }
        public static MvcHtmlString GenerateMetaKeywords(this HtmlHelper html, string part = "")
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            html.AppendMetaKeywordParts(part);
            return MvcHtmlString.Create(html.Encode(pageHeadBuilder.GenerateMetaKeywords()));
        }

        public static void AddCanonicalUrlParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddCanonicalUrlParts(part);
        }
        public static void AppendCanonicalUrlParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendCanonicalUrlParts(part);
        }
        public static MvcHtmlString GenerateCanonicalUrls(this HtmlHelper html, string part = "")
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            html.AppendCanonicalUrlParts(part);
            return MvcHtmlString.Create(pageHeadBuilder.GenerateCanonicalUrls());
        }
        public static void AddHeadCustomParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AddHeadCustomParts(part);
        }
        public static void AppendHeadCustomParts(this HtmlHelper html, string part)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            pageHeadBuilder.AppendHeadCustomParts(part);
        }
        public static MvcHtmlString GenerateHeadCustom(this HtmlHelper html)
        {
            var pageHeadBuilder = IocHelper.Resolve<IPageHeadBuilder>();
            return MvcHtmlString.Create(pageHeadBuilder.GenerateHeadCustom());
        }
    }
}
