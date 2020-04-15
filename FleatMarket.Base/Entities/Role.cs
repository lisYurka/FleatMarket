using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FleatMarket.Base.Entities
{
    public class Role : IdentityRole
    {
        public string RoleName { get; set; }

        public List<User> Roles { get; set; }
    }
}
