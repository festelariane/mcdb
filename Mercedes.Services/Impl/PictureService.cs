using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain.Media;
using System.IO;
using System.Web.Hosting;

namespace Mercedes.Services.Impl
{
    public class PictureService : IPictureService
    {
        private readonly string RootFolder = "~/Uploads";
        public Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, string fileExt, string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true)
        {
            DateTime now = DateTime.UtcNow;
            var folderName = string.Format("Images/{0}", now.ToString("yyyy-MM-dd"));
            var rootPath = HostingEnvironment.MapPath(RootFolder);
            var path = Path.Combine(rootPath, folderName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var saveFile = Path.Combine(path, Guid.NewGuid().ToString() + "_" + DateTime.Now.Ticks.ToString() + fileExt);
            using (var fs = new FileStream(saveFile, FileMode.Create))
            {
                using (var bw = new BinaryWriter(fs))
                {
                    bw.Write(pictureBinary);
                }
            }
            var retVal = new Picture { Url = saveFile.Replace(rootPath, RootFolder),MimeType = mimeType};
            return retVal;
        }
    }
}
