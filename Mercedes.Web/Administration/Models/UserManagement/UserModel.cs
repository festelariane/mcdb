using Mercedes.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mercedes.Admin.Models.UserManagement
{
    public class UserModel : BaseEntityModel
    {
        public UserModel() {
            Roles = new List<RoleViewModel>();
            SelectedRoles = new List<int>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public int LoginAttempts { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool isDeleted { get; set; }
        public List<RoleViewModel> Roles { get; set; }

        public SelectList AllRoles { get; set; }
        public List<int> SelectedRoles { get; set; }
    }
}