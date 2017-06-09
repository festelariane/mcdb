using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public class User: BaseEntity
    {
        public User()
        {
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual string FullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(string.Format("{0} {1}", FirstName, LastName))
                    ? Email
                    : string.Format("{0} {1}", FirstName, LastName);
            }
        }

        public virtual byte[] PasswordHash { get; set; }
        public virtual byte[] PasswordSalt { get; set; }

        public virtual string CurrentEncryption { get; set; }

        public string Email { get; set; }
        public bool IsActive { get; set; }
        public virtual DateTime? LastLoginDate { get; set; }
        public virtual int LoginAttempts { get; set; }

        public Guid? ResetPasswordGuid { get; set; }
        public virtual DateTime? ResetPasswordExpiry { get; set; }
    }
}
