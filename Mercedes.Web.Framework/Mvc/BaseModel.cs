using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Web.Framework.Mvc
{
    public class BaseModel
    {
    }
    public class BaseEntityModel : BaseModel
    {
        public virtual int Id { get; set; }
    }

}
