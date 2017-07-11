using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain.Media
{
    public class Picture:BaseEntity
    {
        public string MimeType { get; set; }
        public string AltAttribute { get; set; }
        public string TitleAttribute { get; set; }
        public string Url { get; set; }
        public string Extension { get; set; }
    }
}
