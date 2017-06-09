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
        public UserManagementService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public void AddUser(User user)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(User user)
        {
            throw new NotImplementedException();
        }

        public Core.Domain.User GetUser(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(User user)
        {
            throw new NotImplementedException();
        }

        public bool IsUniqueEmail(string email, int? id = null)
        {
            throw new NotImplementedException();
        }
    }
}
