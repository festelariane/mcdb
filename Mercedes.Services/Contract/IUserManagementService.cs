using Mercedes.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercedes.Services.Contract
{
    public interface IUserManagementService
    {
        void AddUser(User user);
        void SaveUser(User user);
        User GetUser(int id);
        void DeleteUser(User user);
        bool IsUniqueEmail(string email, int? id = null);
    }
}
