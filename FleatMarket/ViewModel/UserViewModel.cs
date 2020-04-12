using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.ViewModel
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EMail { get; set; }
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
        public string ImagePath { get; set; }

        public string LastEditTime { get; set; }

        public List<UserRoleViewModel> RoleList { get; set; }
    }
}
