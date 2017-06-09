using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Mercedes.Framework.Mvc.ActionResults
{
    public class DownloadResult : ActionResult
    {
        public string VirtualPath { get; set; }
        public string OutputFileName { get; set; }
        public DownloadResult(string virtualPath)
        {
            this.VirtualPath = virtualPath;
        }
        public DownloadResult(string virtualPath, string outputFileName)
        {
            this.VirtualPath = virtualPath;
            this.OutputFileName = outputFileName;
        }
        public override void ExecuteResult(ControllerContext context)
        {
            if (!string.IsNullOrEmpty(OutputFileName))
            {
                context.HttpContext.Response.AddHeader("content-disposition", "attachment; filename=" + this.OutputFileName);
            }
            string path = context.HttpContext.Server.MapPath(this.VirtualPath);
            context.HttpContext.Response.TransmitFile(path);
        }
    }
}
