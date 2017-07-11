using Mercedes.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public partial interface IPictureService
    {
        Picture InsertPicture(byte[] pictureBinary, string mimeType, string seoFilename, string fileExt, string altAttribute = null, string titleAttribute = null, bool isNew = true, bool validateBinary = true);
    }
}
