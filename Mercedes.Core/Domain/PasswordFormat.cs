using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Core.Domain
{
    public enum PasswordFormat
    {
        None = 0,
        Hashed = 1,
        Encrypted = 2
    }
}
