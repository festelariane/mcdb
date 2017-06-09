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
        void AddRole(UserRole role);
        void SaveRole(UserRole role);
        IEnumerable<UserRole> GetAllRoles();
        UserRole GetRoleByName(string name);
        void DeleteRole(UserRole role);
        bool IsOnlyAdmin(User user);
        IEnumerable<UserRole> Find(string term);
        UserRole GetRole(int id);
    }
}
