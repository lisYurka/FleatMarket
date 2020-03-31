using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleatMarket.Web.ViewModel
{
    public class EditUserViewModel
    {

        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required,Phone]
        public string PhoneNumber { get; set; }

        [EmailAddress,Required]
        public string EMail { get; set; }

        public bool IsActive { get; set; }
        public string Role { get; set; }

        public List<UserRoleViewModel> UserRoles { get; set; }
    }
}
