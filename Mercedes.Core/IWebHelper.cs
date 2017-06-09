using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mercedes.Core
{
    public interface IWebHelper
    {
        void RestartAppDomain(bool makeRedirect = false, string redirectUrl = "");
        void RestartCaches();
        string MapPath(string path);
        bool IsStaticResource(HttpRequest request);
    }
}
