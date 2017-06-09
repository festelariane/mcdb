using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public abstract class BaseEntity
    {
        private Guid _guid;
        protected BaseEntity()
        {
            _guid = Guid.NewGuid();
        }
        public virtual int Id { get; set; }

        public virtual Guid Guid
        {
            get { return _guid; }
        }

        public virtual DateTime CreatedOn { get; set; }

        public virtual DateTime UpdatedOn { get; set; }

        public virtual bool IsDeleted { get; set; }

        public virtual void SetGuid(Guid guid)
        {
            _guid = guid;
        }
    }
}
