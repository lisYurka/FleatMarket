﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FleatMarket.Base.Entities
{
    //public class Role//:IdentityRole
    //{
    //    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    //    public int Id { get; set; }
    //    public string RoleName { get; set; }

    //    public List<User> Users { get; set; }
    //}

    public class Role : IdentityRole
    {
        public string RoleName { get; set; }

        public List<User> Roles { get; set; }
    }
}
