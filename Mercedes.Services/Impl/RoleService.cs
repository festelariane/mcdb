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
        public bool SaveRole(UserRole role)
        {
            try
            {
                if (role.Id > 0)
                {
                    _roleRepository.Update(role);
                }
                else
                {
                    _roleRepository.Add(role);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public IEnumerable<UserRole> GetAllRoles()
        {
            return _roleRepository.GetAll().ToList();
        }

        public UserRole GetRoleByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool DeleteRole(UserRole role)
        {
            try
            {
                _roleRepository.Delete(role);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
            return _roleRepository.Get(id);
        }
        public bool AssignUserToRole(User user, UserRole role)
        {
            return _roleRepository.AssignUserToRole(user, role);
        }
        public bool RemoveUserRoles(User user, List<int> roleIdList)
        {
            return _roleRepository.RemoveUserRoles(user, roleIdList);
        }
    }
}
