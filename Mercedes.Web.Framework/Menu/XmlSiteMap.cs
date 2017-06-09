

using Mercedes.Core;
using Mercedes.Core.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using System.Xml;

namespace Mercedes.Framework.Menu
{
    public class XmlSiteMap
    {
        private readonly string _fileName;
        //private readonly Func<IList<string>, bool> _funcHasRole;
        public XmlSiteMap(string fileName, Func<IList<string>, bool> funcHasRole = null)
        {
            _fileName = fileName;
            RootNode = new SiteMapNode();
            LoadFrom(_fileName, funcHasRole);
        }

        public SiteMapNode RootNode { get; set; }
        private SiteMapNode _currentNode = null;
        private bool _currentNodeSet = false;
        public SiteMapNode CurrentNode 
        {
            get 
            {
                if(_currentNodeSet)
                {
                    return _currentNode;
                }
                var context = IocHelper.Resolve<HttpContextBase>();
                var routeData = context.Request.RequestContext.RouteData;
                var currentAction = routeData.Values["action"].ToString();
                var currentController = routeData.Values["controller"].ToString();
                var currentArea = context.Request.RequestContext.RouteData.DataTokens["area"].ToString();

                if(this.RootNode == null)
                {
                    _currentNodeSet = true;
                    return null;
                }

                var item = this.RootNode;
                var stack = new Stack<SiteMapNode>();
                stack.Push(item);
                while (stack.Any())
                {
                    var next = stack.Pop();
                    if (string.Compare(next.ControllerName, currentController, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.Compare(next.ActionName, currentAction, StringComparison.OrdinalIgnoreCase) == 0 &&
                    string.Compare("Admin", currentArea, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        _currentNodeSet = true;
                        _currentNode = next;
                        return next;
                    }

                    foreach (var child in next.ChildNodes)
                    {
                        stack.Push(child);
                    }
                }
                _currentNodeSet = true;
                return null;
            } 
        }
        private static IEnumerable<T> Traverse<T>(T item, Func<T,IEnumerable<T>> childSelector)
        {
            var stack = new Stack<T>();
            stack.Push(item);
            while(stack.Any())
            {
                var next = stack.Pop();
                yield return next;
                foreach(var child in childSelector(next))
                {
                    stack.Push(child);
                }
            }
        }

        protected virtual void LoadFrom(string physicalPath, Func<IList<string>, bool> funcHasRole = null)
        {
            var webHelper = IocHelper.Resolve<IWebHelper>();
            string filePath = webHelper.MapPath(physicalPath);
            var context = IocHelper.Resolve<HttpContextBase>();
            string content = File.ReadAllText(filePath);

            if (!string.IsNullOrEmpty(content))
            {
                using (var sr = new StringReader(content))
                {
                    using (var xr = XmlReader.Create(sr,
                            new XmlReaderSettings
                            {
                                CloseInput = true,
                                IgnoreWhitespace = true,
                                IgnoreComments = true,
                                IgnoreProcessingInstructions = true
                            }))
                    {
                        var doc = new XmlDocument();
                        doc.Load(xr);

                        if ((doc.DocumentElement != null) && doc.HasChildNodes)
                        {
                            XmlNode xmlRootNode = doc.DocumentElement.FirstChild;
                            Iterate(RootNode, xmlRootNode, funcHasRole);
                        }
                    }
                }
            }
        }

        private static void Iterate(SiteMapNode siteMapNode, XmlNode xmlNode, Func<IList<string>, bool> funcHasRole)
        {
            PopulateNode(siteMapNode, xmlNode, funcHasRole);

            foreach (XmlNode xmlChildNode in xmlNode.ChildNodes)
            {
                if (xmlChildNode.LocalName.Equals("siteMapNode", StringComparison.InvariantCultureIgnoreCase))
                {
                    var siteMapChildNode = new SiteMapNode();
                    siteMapNode.ChildNodes.Add(siteMapChildNode);
                    siteMapChildNode.ParentNode = siteMapNode;

                    Iterate(siteMapChildNode, xmlChildNode, funcHasRole);
                }
            }
        }

        private static void PopulateNode(SiteMapNode siteMapNode, XmlNode xmlNode, Func<IList<string>,bool> funcHasRole)
        {
            siteMapNode.SystemName = GetStringValueFromAttribute(xmlNode, "SystemName");
            siteMapNode.Title = GetStringValueFromAttribute(xmlNode, "Title");
            string controllerName = GetStringValueFromAttribute(xmlNode, "controller");
            string actionName = GetStringValueFromAttribute(xmlNode, "action");
            string url = GetStringValueFromAttribute(xmlNode, "url");
            if (!string.IsNullOrEmpty(controllerName) && !string.IsNullOrEmpty(actionName))
            {
                siteMapNode.ControllerName = controllerName;
                siteMapNode.ActionName = actionName;

                siteMapNode.RouteValues = new RouteValueDictionary { { "area", "Admin" } };
            }
            else if (!string.IsNullOrEmpty(url))
            {
                siteMapNode.Url = url;
            }

            siteMapNode.ImageUrl = GetStringValueFromAttribute(xmlNode, "ImageUrl");

            var strRoles = GetStringValueFromAttribute(xmlNode, "Roles");
            if (!string.IsNullOrEmpty(strRoles))
            {
                if(funcHasRole != null)
                {
                    var roles = strRoles.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    siteMapNode.Visible = funcHasRole(roles);
                }
                else
                {
                    siteMapNode.Visible = false;
                }
            }
            else
            {
                siteMapNode.Visible = true;
            }
        }

        private static string GetStringValueFromAttribute(XmlNode node, string attributeName)
        {
            string value = null;

            if (node.Attributes != null && node.Attributes.Count > 0)
            {
                XmlAttribute attribute = node.Attributes[attributeName];

                if (attribute != null)
                {
                    value = attribute.Value;
                }
            }

            return value;
        }
    }
}
