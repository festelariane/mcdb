using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface IRoleService
    {
        bool SaveRole(UserRole role);
        IEnumerable<UserRole> GetAllRoles();
        UserRole GetRoleByName(string name);
        bool DeleteRole(UserRole role);
        bool IsOnlyAdmin(User user);
        IEnumerable<UserRole> Find(string term);
        UserRole GetRole(int id);
        bool AssignUserToRole(User user, UserRole role);
        bool RemoveUserRoles(User user, List<int> roleIdList);
    }
}
