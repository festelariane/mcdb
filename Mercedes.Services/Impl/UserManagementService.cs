using Mercedes.Core.Domain;
using Mercedes.Data.Repositories.Contract;
using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Impl
{
    public class UserManagementService: IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserManagementService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        public bool UpdateUser(User user)
        {
            try
            {
                user.UpdatedOn = DateTime.Now;
                _userRepository.Update(user);
                
                return true;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public void DeleteUser(User user)
        {
            _userRepository.Delete(user);
        }

        public bool IsUniqueEmail(string email, int? id = null)
        {
            throw new NotImplementedException();
        }
        public IList<User> GetAll(bool includeDeletedUsers = false, int pageIndex = 0, int pageSize = 10000)
        {
            try
            {
                return _userRepository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
