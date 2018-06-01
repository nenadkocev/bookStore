using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bookStore.Models
{
    public class AddUserToRoleModel
    {
        public List<string> Roles { get; set; }
        public string selectedRole { get; set; }
        public string userEmail { get; set; }

        public AddUserToRoleModel()
        {
            Roles = new List<string>{ Role.Administrator, Role.Seller, Role.User};
        }
    }
}