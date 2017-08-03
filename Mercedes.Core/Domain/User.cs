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
            Roles = new List<UserRole>();
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

        public string Password { get; set; }
        public string PasswordSalt { get; set; }

        public int PasswordFormatId { get; set; }
        public Guid UserGuid { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int LoginAttempts { get; set; }
        public List<UserRole> Roles { get; set; }
    }
}
