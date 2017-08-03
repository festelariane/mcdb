using Mercedes.Core.Domain;
using Mercedes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface IUserRegistrationService
    {
        UserLoginResults ValidateUser(string usernameOrEmail, string password);
        bool RegisterUser(UserRegistrationRequest request);
        bool ChangePassword(ChangePasswordRequest request);
    }
}
