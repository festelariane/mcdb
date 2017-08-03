using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Data.Repositories.Contract
{
    public interface IRoleRepository : IGenericRepository<UserRole>
    {
        bool AssignUserToRole(User user, UserRole role);
        bool RemoveUserRoles(User user, List<int> roleIdList);
    }
}
