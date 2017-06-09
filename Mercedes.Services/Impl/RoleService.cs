using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;

namespace Mercedes.Services.Impl
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public void AddRole(UserRole role)
        {
            throw new NotImplementedException();
        }
        public void SaveRole(UserRole role)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public UserRole GetRoleByName(string name)
        {
            throw new NotImplementedException();
        }

        public void DeleteRole(UserRole role)
        {
            throw new NotImplementedException();
        }

        public bool IsOnlyAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> Find(string term)
        {
            throw new NotImplementedException();
        }

        public UserRole GetRole(int id)
        {
            throw new NotImplementedException();
        }
    }
}
