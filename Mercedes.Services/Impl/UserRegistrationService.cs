using Mercedes.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mercedes.Core.Domain;
using Mercedes.Services.Models;
using Mercedes.Services.Contract.Security;
using Mercedes.Data.Repositories.Contract;

namespace Mercedes.Services.Impl
{
    public class UserRegistrationService : IUserRegistrationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IRoleRepository _roleRepository;
        public UserRegistrationService(IUserRepository userRepository, IEncryptionService encryptionService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _encryptionService = encryptionService;
            _roleRepository = roleRepository;
        }
        public bool ChangePassword(ChangePasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public bool RegisterUser(UserRegistrationRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request cannot be null");

            try
            {
                //Valid request. Continue proccessing.
                User u = new User();
                u.UserGuid = Guid.NewGuid();
                u.UserName = request.Username;
                u.FirstName = request.FirstName;
                u.LastName = request.LastName;
                u.Email = request.Email;
                if (string.IsNullOrEmpty(u.UserName))
                    u.UserName = u.Email;
                u.IsActive = request.IsApproved;
                switch (request.PasswordFormat)
                {
                    case PasswordFormat.None:
                        u.Password = request.Password;
                        break;
                    case PasswordFormat.Encrypted:
                        u.Password = _encryptionService.EncryptText(request.Password);
                        break;
                    case PasswordFormat.Hashed:
                        u.PasswordSalt = _encryptionService.CreateSaltKey(8);
                        u.Password = _encryptionService.CreatePasswordHash(request.Password, u.PasswordSalt);
                        break;
                }
                u.PasswordFormatId = (int)request.PasswordFormat;
                u.CreatedOn = DateTime.Now;
                _userRepository.Add(u);
                //Assign roles:
                if(request.Roles != null && request.Roles.Count > 0)
                {
                    foreach(var role in request.Roles)
                    {
                        _roleRepository.AssignUserToRole(u,role);
                    }
                }
                return true;
            }
            catch (Exception  ex)
            {
                return false;
            }
        }

        public UserLoginResults ValidateUser(string usernameOrEmail, string password)
        {
            throw new NotImplementedException();
        }
    }
}
